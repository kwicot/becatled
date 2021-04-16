using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachine
{
    public interface ICharacterBehavior
    {
        public CharacterBase characterBase { get; set; }
        public Animator _animator { get; set; }
        void Enter(CharacterBase characterBase,Animator animator);
        void Exit();
        void Update();

        void FixedUpdate();

    }
}
