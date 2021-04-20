using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Becatled.Character;
using Becatled.CharacterCore.StateMachineCore;
using Pathfinding;
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

        void Start()
        {
            RagdollOff();
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
            //for (int i = L_CharacterBase.Count-1; i >= 0; i--)
           //if(L_CharacterBase[i] == null) L_CharacterBase.RemoveAt(i);
        
           // if (L_CharacterBase.Count > 0)
           // {
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
               foreach (var enemy in L_CharacterBase)
               {
                   if (enemy == null)
                   {
                       L_CharacterBase.Remove(enemy);
                       break;
                   }
               }

               return null;
           }
           catch (MissingReferenceException e)
           {
               Debug.LogWarning("Deleted- "+e);
               foreach (var enemy in L_CharacterBase)
               {
                   if (enemy == null)
                   {
                       L_CharacterBase.Remove(enemy);
                       break;
                   }
               }

               return null;
           }
                
           // }
           // return null;
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
            if(SelectedEnemy != null)
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
                if (main != target)
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

        public void RagdollOn()
        {
            Destroy(trigger);
            Destroy(gameObject.GetComponent<CharacterController>());
            Destroy(_animator);
            Destroy(gameObject.GetComponent<AIDestinationSetter>());
            Destroy(gameObject, 3f);

            
            
            Collider[] colls = gameObject.GetComponentsInChildren<Collider>();
            foreach (var col in colls)
            {
                col.enabled = true;
            }
            
            Rigidbody[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
            foreach (var obj in rigidbodies)
            {
                obj.isKinematic = false;
            }
            Destroy(this);
        }

        public void RagdollOff()
        {
            Collider[] colls = gameObject.GetComponentsInChildren<Collider>();
            foreach (var col in colls)
            {
                if (col != trigger && col != gameObject.GetComponent<CharacterController>())
                {
                    col.gameObject.layer = 8;
                    col.enabled = false;
                }
            }

            Rigidbody[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
            foreach (var obj in rigidbodies)
            {
                obj.isKinematic = true;
            }
        }
    }
}
