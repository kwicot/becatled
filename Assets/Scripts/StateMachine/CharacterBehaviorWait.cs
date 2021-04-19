using System;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachine
{
    public class CharacterBehaviorWait : MonoBehaviour, ICharacterBehavior
    {
        public CharacterBase characterBase { get; set; }
        public Animator _animator { get; set; }

        private float timeToAttack;
        public void Enter(CharacterBase controller,Animator animator)
        {
            characterBase = controller;
            _animator = animator;
            _animator.Play("Idle");
            timeToAttack = controller._model.AttackSpeed;
        }

        public void Exit()
        {
        }

        public void Update()
        {
            timeToAttack -= Time.deltaTime;
            if (timeToAttack <= 0)
            {
                characterBase.SetBehaviorIdle();
            }
        }

        public void FixedUpdate()
        {
        }

        
    }
}