using System;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachine
{
    public class CharacterBehaviorAggressive : MonoBehaviour, ICharacterBehavior
    {
        public CharacterBase CharacterBase { get; set; }
        public Animator _animator { get; set; }

        public void Enter(CharacterBase characterBase,Animator animator)
        {
            CharacterBase = characterBase;
            _animator = animator;
            Debug.Log(("Enter aggressive behavior"));
        }

        public void Exit()
        {
            Debug.Log(("Exit aggressive behavior"));
        }

        public void Update()
        {
            Debug.Log(("Update aggressive behavior"));
        }

        public void FixedUpdate()
        {
            throw new NotImplementedException();
        }
    }
}