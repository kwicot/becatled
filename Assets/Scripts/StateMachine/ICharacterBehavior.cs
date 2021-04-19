using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachineCore
{
    public interface ICharacterBehavior
    {
        public CharacterBase Character { get; set; }
        void Enter(CharacterBase controller,CharacterBase _enemy = null);
        void Exit();
        void Update();

        void FixedUpdate();

    }
}
