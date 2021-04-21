using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Becatled.Character;
using Becatled.CharacterCore.StateMachineCore;
using Pathfinding;
using Unity.Mathematics;
using UnityEngine;

namespace Becatled.CharacterCore
{
    public class CharacterBase : MonoBehaviour
    {
        
        /*
         * Базовый класс персонажей
         * Вся основная логика которая не имеет привязанности к конкретному типу персонажей
         * должна находится здесь
         * 
         */
        public StateMachine stateMachine { get; protected set; } //Машина состояний
        public Character_Model _model { get; protected set; } //Модель данных
        public CharacterBase SelectedEnemy { get; set; } //Ближайший враг  
        public Animator _animator { get; private set; } 
        public AIDestinationSetter AI { get; private set; } //Навигация

        public List<CharacterBase> L_CharacterBase { get; private set; } //Список врагов в области видимости
        public CapsuleCollider trigger;
        public RagdollController RagdollController; //Контроллер тряпичной куклы


        public ParticleSystem BloodParticles;

            /*
             * Выключаем режим тряпичной куклы
             * инициализируем навигатор, аниматор, лист врагов и машину состояний
             * И вызываем виртуальный метод инициализации для наследуемых классов
             */
        void Start()
        {
            RagdollController.RagdollOFF();
            AI = GetComponent<AIDestinationSetter>();
            _animator = GetComponent<Animator>();
            L_CharacterBase = new List<CharacterBase>();
            Init();
            stateMachine = new StateMachine(this, _animator);
        }

        private float customUpdateInterval = 4; //Интервал кастомных обновлений (Количество обновлений в секунду)
        private float customUpdateTime = 0.20f; //таймер кастомного обновления 
        private void Update()
        {
            stateMachine.Update();
            customUpdateTime -= Time.deltaTime;
            if(customUpdateTime <= 0) CustomUpdate();
        }

        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }

        void CustomUpdate()
        {
            customUpdateTime = 60 / customUpdateInterval;
            
            stateMachine.behaviorCurrent.CustomUpdate();
        }

        protected virtual void Init()
        {

        }

        /*
         * Поиск ближайшего врага
         * Установка состояния в зависимости от исхода
         */
        public CharacterBase GetClosets()
        {
            try
            {
                CharacterBase closets = null;
                float bestDistance = 1000;
                foreach (var selected in L_CharacterBase)
                {
                    float distance = Vector3.Distance(transform.position, selected.transform.position);
                    if (distance < bestDistance)
                    {
                        closets = selected;
                        bestDistance = distance;
                    }
                }

                return closets;
            }
            catch (NullReferenceException e)
            {
                Debug.LogWarning(e);
                for (int i = L_CharacterBase.Count - 1; i >= 0; i--)
                {
                    if (L_CharacterBase[i] == null)
                        L_CharacterBase.RemoveAt(i);
                }

                return null;
            }
            catch (MissingReferenceException e)
            {
                Debug.LogWarning("Deleted- " + e);
                for (int i = L_CharacterBase.Count - 1; i >= 0; i--)
                {
                    if (L_CharacterBase[i] == null)
                        L_CharacterBase.RemoveAt(i);
                }

                return null;
            }
        }

        public void MakeDamage(float dmg,bool withEffect = true)
        {
            _model.HP -= dmg;

            if (withEffect)
            {
                quaternion rot = transform.rotation;
                Instantiate(BloodParticles, transform.position, rot).Play();
            }
            
            if (_model.HP <= 0)
                Death();
        }

        public void DoMeleeDamage() //Метод аниматора
        {
            Debug.Log("Attack");
            if (SelectedEnemy != null)
                SelectedEnemy.MakeDamage(_model.Damage);
        }

        public void EndMeleeAttack() //Метод аниматора
        {
            Debug.Log("End attack" + SelectedEnemy);
            if (SelectedEnemy != null)
                stateMachine.SetBehaviorWait(SelectedEnemy);
            else stateMachine.SetBehaviorIdle();
        }

        // уничтожаем всё лишнее и включаем тряпичную куклу
        public void Death()
        {
            Destroy(_animator);
            Destroy(this);
            Destroy(gameObject, 5f);
            Destroy(AI);
            RagdollController.RagdollON();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterBase characterBase))
            {
                var main = this.GetType();
                var target = characterBase.GetType();
                if (main != target)
                {
                    L_CharacterBase.Add(characterBase);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CharacterBase characterBase))
            {
                L_CharacterBase.Remove(characterBase);
            }
        }
    }
}
