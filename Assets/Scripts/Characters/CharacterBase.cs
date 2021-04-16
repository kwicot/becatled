using System;
using System.Collections;
using System.Collections.Generic;
using Becatled.Character;
using Becatled.CharacterCore.StateMachine;
using Pathfinding;
using UnityEngine;

namespace Becatled.CharacterCore
{
    [RequireComponent(
        typeof(Animator),
        typeof(CapsuleCollider),
        typeof(CharacterController)
        
        )]
    public class CharacterBase : MonoBehaviour
    {
        protected Dictionary<Type, ICharacterBehavior> behaviorsMap;
        protected ICharacterBehavior behaviorCurrent;

        public Character_Model _model;
        public CharacterBase SelectedEnemy;
        public Animator _animator { get; private set; }
        public AIDestinationSetter AI { get; private set; }

        public List<CharacterBase> L_CharacterBase { get; private set; }
        public CapsuleCollider trigger;

        void Start()
        {
            AI = GetComponent<AIDestinationSetter>();
            _animator = GetComponent<Animator>();
            L_CharacterBase = new List<CharacterBase>();
            
            InitBehaviors();
            SetBehaviorIdle();
            Init();
        }

        void Update()
        {
            if (behaviorCurrent != null) behaviorCurrent.Update();
        }

        private void FixedUpdate()
        {
            if(behaviorCurrent != null) behaviorCurrent.FixedUpdate();
        }

        protected virtual void Init()
        {
            
        }
        

        protected void InitBehaviors()
        {
            behaviorsMap = new Dictionary<Type, ICharacterBehavior>();
            
            behaviorsMap[typeof(CharacterBehaviorIdle)] = new CharacterBehaviorIdle();
            behaviorsMap[typeof(CharacterBehaviorWait)] = new CharacterBehaviorWait();
            behaviorsMap[typeof(CharacterBehaviorAttack)] = new CharacterBehaviorAttack();
            behaviorsMap[typeof(CharacterBehaviorAggressive)] = new CharacterBehaviorAggressive();
        }
        protected void SetBehavior(ICharacterBehavior behavior)
        {
            if (behaviorCurrent != null)
                behaviorCurrent.Exit();

            behaviorCurrent = behavior;
            behaviorCurrent.Enter(this,_animator);
        }
        protected ICharacterBehavior GetBehavior<T>() where T : ICharacterBehavior
        {
            var type = typeof(T);
            return behaviorsMap[type];
        }
        public void SetBehaviorIdle()
        {
            var behavior = GetBehavior<CharacterBehaviorIdle>();
            SetBehavior(behavior);
        }

        public void SetBehaviorAggressive()
        {
            var behavior = GetBehavior<CharacterBehaviorAggressive>();
            SetBehavior(behavior);
        }

        public void SetBehaviorWait()
        {
            var behavior = GetBehavior<CharacterBehaviorWait>();
            SetBehavior(behavior);
        }

        public void SetBehaviorAttack()
        {
            var behavior = GetBehavior<CharacterBehaviorAttack>();
            SetBehavior(behavior);
        }
        public Transform GetClosets()
        {
            if (L_CharacterBase.Count > 0)
            {
                Transform closets = L_CharacterBase[0].transform;
                float bestdis = Vector3.Distance(transform.position, closets.position);
                foreach (var selected in L_CharacterBase)
                {
                    float distance = Vector3.Distance(transform.position, selected.transform.position);
                    if (distance < bestdis)
                    {
                        closets = selected.transform;
                        bestdis = distance;
                    }
                }
                return closets;
            }
            return null;
        }

        public void MakeDamage(float dmg)
        {
            _model.HP -= dmg;
            if (_model.HP <= 0)
            {
                //TODO смерть
            }
        }
        public void DoMeleeDamage()
        {
            Debug.Log("Attack");
            SelectedEnemy.MakeDamage(_model.Damage);
        }

        public void EndMeleeAttack()
        {
            Debug.Log("End attack");
            SetBehaviorWait();
        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out CharacterBase characterBase))
            { 
                var main = this.GetType();
                var target = characterBase.GetType();
                if (other.gameObject != gameObject && main != target)
                {
                    L_CharacterBase.Add(characterBase);
                    Debug.Log(other.name);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out CharacterBase characterBase))
            {
                L_CharacterBase.Remove(characterBase);
            }
        }
    }
}
