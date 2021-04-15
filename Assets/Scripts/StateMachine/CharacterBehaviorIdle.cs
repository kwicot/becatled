using System;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachine
{
    public class CharacterBehaviorIdle : MonoBehaviour, ICharacterBehavior
    {
        public CharacterBase CharacterBase { get; set; }
        public Animator _animator { get; set; }

        public void Enter(CharacterBase characterBase,Animator animator)
        {
            CharacterBase = characterBase;
            _animator = animator;
            Debug.Log(("Enter idle behavior"));
        }

        public void Exit()
        {
            Debug.Log(("Exit idle behavior"));
        }

        public void Update()
        {
            Debug.Log(("Update idle behavior"));
        }

        public void FixedUpdate()
        {
            throw new NotImplementedException();
        }
    }
}