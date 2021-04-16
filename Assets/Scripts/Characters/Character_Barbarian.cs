using Becatled.CharacterCore;
using UnityEngine;

namespace Becatled.Character
{
    public class Character_Barbarian : CharacterBase
    {
        protected override void Init()
        {
            _model = new Character_Model()
            {
                MoveSpeed = 10,
                Damage = 10,
                HP = 100,
                AggressiveDistance = 10,
                AttackDistance = 1
            };
            trigger.radius = _model.AggressiveDistance;
            trigger.isTrigger = true;
        }
    }
}