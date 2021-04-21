using System;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachineCore
{
    public class CharacterBehaviorAttack : ICharacterBehavior
    {
        public CharacterBase Character { get; set; }
        private CharacterBase enemy;

        public void Enter(CharacterBase controller,CharacterBase _enemy = null)
        {
            //Debug.Log(("Enter attack behavior"));
            Character = controller;
            Character._animator.Play("Attack");
        }

        public void Exit()
        {
            //Debug.Log(("Exit attack behavior"));
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
        }

        public void CustomUpdate()
        {
            
        }
    }
}