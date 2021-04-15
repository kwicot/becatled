using System;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachine
{
    public class CharacterBehaviorAttack : MonoBehaviour, ICharacterBehavior
    {
        public CharacterBase CharacterBase { get; set; }
        public Animator _animator { get; set; }

        public void Enter(CharacterBase characterBase,Animator animator)
        {
            CharacterBase = characterBase;
            _animator = animator;
            Debug.Log(("Enter attack behavior"));
        }

        public void Exit()
        {
            Debug.Log(("Exit attack behavior"));
        }

        public void Update()
        {
            Debug.Log(("Update attack behavior"));
        }

        public void FixedUpdate()
        {
            throw new NotImplementedException();
        }
    }
}