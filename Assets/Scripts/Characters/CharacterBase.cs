using System;
using System.Collections;
using System.Collections.Generic;
using Becatled.Character;
using Becatled.CharacterCore.StateMachineCore;
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
        public StateMachine stateMachine { get; protected set; }
        public Character_Model _model { get; protected set; }
        public CharacterBase SelectedEnemy { get; set; }
        public Animator _animator { get; private set; }
        public AIDestinationSetter AI { get; private set; }

        public List<CharacterBase> L_CharacterBase { get; private set; }
        public CapsuleCollider trigger;

        void Start()
        {
            AI = GetComponent<AIDestinationSetter>();
            _animator = GetComponent<Animator>();
            L_CharacterBase = new List<CharacterBase>();
            Init();
            stateMachine = new StateMachine(this, _animator);
        }

        private void Update()
        {
            stateMachine.Update();
        }

        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }

        protected virtual void Init()
        {
            
        }
        public CharacterBase GetClosets()
        {
            if (L_CharacterBase.Count > 0)
            {
                CharacterBase closets = L_CharacterBase[0];
                float bestDistance = Vector3.Distance(transform.position, closets.transform.position);
                foreach (var selected in L_CharacterBase)
                {
                    float distance = Vector3.Distance(transform.position, selected.transform.position);
                    var enemyState = selected.stateMachine.behaviorCurrent;
                    var deathState = stateMachine.GetBehavior<CharacterBehaviorDeath>();
                    if (enemyState == deathState)
                    {
                        L_CharacterBase.Remove(selected);
                        continue;
                    }
                    if (distance < bestDistance)
                    {
                        closets = selected;
                        bestDistance = distance;
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
                stateMachine.SetBehaviorDeath();
            }
        }
        public void DoMeleeDamage()
        {
            Debug.Log("Attack");
            SelectedEnemy.MakeDamage(_model.Damage);
        }

        public void EndMeleeAttack()
        {
            Debug.Log("End attack" + SelectedEnemy);
            if(SelectedEnemy != null)
                stateMachine.SetBehaviorWait(SelectedEnemy);
            else stateMachine.SetBehaviorIdle();
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
