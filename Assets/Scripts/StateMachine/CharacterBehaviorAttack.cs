using System;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachine
{
    public class CharacterBehaviorAttack : MonoBehaviour, ICharacterBehavior
    {
        public CharacterBase characterBase { get; set; }
        public Animator _animator { get; set; }

        private CharacterBase enemy;

        public void Enter(CharacterBase controller,Animator animator)
        {
            characterBase = controller;
            _animator = animator;
            _animator.Play("Attack");
            Debug.Log(("Enter attack behavior"));
            controller.SelectedEnemy = this.characterBase.GetClosets().GetComponent<CharacterBase>();
        }

        public void Exit()
        {
            Debug.Log(("Exit attack behavior"));
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
        }
        
        
    }
}