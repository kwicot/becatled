using System;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Becatled.CharacterCore.StateMachine
{
    public class CharacterBehaviorAggressive : MonoBehaviour, ICharacterBehavior
    {
        public CharacterBase characterBase { get; set; }
        public Animator _animator { get; set; }

        public void Enter(CharacterBase _characterBase,Animator animator)
        {
            Debug.Log(("Enter aggressive behavior"));
            characterBase = _characterBase;
            _animator = animator;
            var a = characterBase.GetClosets();
            if (a != null)
                characterBase.AI.target = a;
            _animator.Play("Run");
        }

        public void Exit()
        {
            Debug.Log(("Exit aggressive behavior"));
        }

        public void Update()
        {
            var closets = characterBase.GetClosets();
            if (closets != null)
            {
                var dis = Vector3.Distance(characterBase.transform.position,
                    closets.position);
                if (dis < characterBase._model.AttackDistance)
                    characterBase.SetBehaviorAttack();
            }
        }

        public void FixedUpdate()
        {
            
        }
    }
}