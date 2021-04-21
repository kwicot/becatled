using System;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachineCore
{
    public class CharacterBehaviorIdle : ICharacterBehavior
    {
        public CharacterBase Character { get; set; }

        public void Enter(CharacterBase controller,CharacterBase _enemy = null)
        {
            //Debug.Log("Enter idle behavior");
            Character = controller;
            Character._animator.Play("Idle");
        }

        public void Exit()
        {
            //Debug.Log("Exit idle behavior");
        }

        public void Update()
        {
            
        }
        

        public void FixedUpdate()
        {
        }

        public void CustomUpdate()
        {
            var closets = Character.GetClosets();
            if (closets != null)
            {
                Character.SelectedEnemy = closets;
                var dis = Vector3.Distance(Character.transform.position,
                    closets.transform.position);
                if (dis < Character._model.AggressiveDistance)
                {
                    if (dis < Character._model.AttackDistance)
                    {
                        Character.stateMachine.SetBehaviorAttack(closets);
                    }
                    else Character.stateMachine.SetBehaviorAggressive(closets);
                }
            }
            else Character.SelectedEnemy = null;
        }
    }
}