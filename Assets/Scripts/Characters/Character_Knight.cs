using UnityEngine;
using Becatled.CharacterCore;

namespace Becatled.Character
{
    public class Character_Knight : CharacterBase
    {
        protected override void Init()
        {
            _model = new Character_Model() //Инициализируем модель данных
            {
                MoveSpeed = 10,
                Damage = 10,
                HP = 20,
                AggressiveDistance = 10,
                AttackDistance = 1,
                AttackSpeed = 0.5f
            };
            trigger.radius = _model.AggressiveDistance;
            trigger.isTrigger = true;
        }
    }
}