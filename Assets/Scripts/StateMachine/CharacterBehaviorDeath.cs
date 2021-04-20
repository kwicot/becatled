using System;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachineCore
{
    public class CharacterBehaviorDeath : ICharacterBehavior
    {
        public CharacterBase Character { get; set; }
        public void Enter(CharacterBase controller,CharacterBase _enemy = null)
        {
            Character = controller;
            
            Character.RagdollOn();
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
        }
    }
}