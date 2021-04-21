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
        public StateMachine stateMachine { get; protected set; }
        public Character_Model _model { get; protected set; }
        public CharacterBase SelectedEnemy { get; set; }
        public Animator _animator { get; private set; }
        public AIDestinationSetter AI { get; private set; }

        public List<CharacterBase> L_CharacterBase { get; private set; }
        public CapsuleCollider trigger;
        public RagdollController RagdollController;


        public ParticleSystem BloodParticles;

        void Start()
        {
            RagdollController.RagdollOFF();
            AI = GetComponent<AIDestinationSetter>();
            _animator = GetComponent<Animator>();
            L_CharacterBase = new List<CharacterBase>();
            Init();
            stateMachine = new StateMachine(this, _animator);
        }

        private float inteval = 0.20f;
        private void Update()
        {
            stateMachine.Update();
            inteval -= Time.deltaTime;
            if(inteval <= 0) CustomUpdate();
        }

        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }

        void CustomUpdate()
        {
            inteval = 0.20f;
            
            stateMachine.behaviorCurrent.CustomUpdate();
        }

        protected virtual void Init()
        {

        }

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

        public void DoMeleeDamage()
        {
            Debug.Log("Attack");
            if (SelectedEnemy != null)
                SelectedEnemy.MakeDamage(_model.Damage);
        }

        public void EndMeleeAttack()
        {
            Debug.Log("End attack" + SelectedEnemy);
            if (SelectedEnemy != null)
                stateMachine.SetBehaviorWait(SelectedEnemy);
            else stateMachine.SetBehaviorIdle();
        }

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
