using System;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachine
{
    public class CharacterBehaviorAttack : MonoBehaviour, ICharacterBehavior
    {
        public CharacterBase characterBase { get; set; }
        public Animator _animator { get; set; }

        private CharacterBase enemy;

        public void Enter(CharacterBase characterBase,Animator animator)
        {
            this.characterBase = characterBase;
            _animator = animator;
            _animator.Play("Attack");
            Debug.Log(("Enter attack behavior"));
            characterBase.SelectedCharacter = this.characterBase.GetClosets().GetComponent<CharacterBase>();
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