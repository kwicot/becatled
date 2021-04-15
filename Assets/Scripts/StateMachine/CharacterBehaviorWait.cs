using System;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachine
{
    public class CharacterBehaviorWait : MonoBehaviour, ICharacterBehavior
    {
        public CharacterBase CharacterBase { get; set; }
        public Animator _animator { get; set; }

        public void Enter(CharacterBase characterBase,Animator animator)
        {
            CharacterBase = characterBase;
            _animator = animator;
            Debug.Log(("Enter wait behavior"));
        }

        public void Exit()
        {
            Debug.Log(("Exit wait behavior"));
        }

        public void Update()
        {
            Debug.Log(("Update wait behavior"));
        }

        public void FixedUpdate()
        {
            throw new NotImplementedException();
        }
    }
}