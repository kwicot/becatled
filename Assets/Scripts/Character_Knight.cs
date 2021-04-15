using UnityEngine;
using Becatled.CharacterCore;

namespace Becatled.Character
{
    public class Character_Knight : CharacterBase
    {
        protected override void Init()
        {
            _model = new Character_Model()
            {
                MoveSpeed = 10,
                Damage = 10,
                HP = 100
            };
        }
    }
}