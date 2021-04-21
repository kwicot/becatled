using System;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachineCore
{
    public class CharacterBehaviorWait : ICharacterBehavior
    {
        /*
         * Состояние подготовки к следующей атаке
         */
        public CharacterBase Character { get; set; }
        private CharacterBase enemy;

        private float timeToAttack;
        public void Enter(CharacterBase controller,CharacterBase _enemy = null)
        {
            //Debug.Log("Enter wait state");
            Character = controller;
            timeToAttack = Character._model.AttackSpeed;
            enemy = _enemy;
            Character._animator.Play("Wait");
        }

        public void Exit()
        {
            //Debug.Log("Exit wait state");
        }


        public void Update()
        {
            try
            {
                timeToAttack -= Time.deltaTime;
                if (enemy == null)
                {
                    Character.stateMachine.SetBehaviorIdle();
                    Character.SelectedEnemy = null;
                }
                else if (timeToAttack <= 0 && enemy != null)
                {
                    
                    var dis = Vector3.Distance(Character.transform.position,
                        enemy.transform.position);
                    if (dis < Character._model.AggressiveDistance)
                    {
                        if (dis < Character._model.AttackDistance)
                        {
                            Character.stateMachine.SetBehaviorAttack(enemy);
                        }
                        else Character.stateMachine.SetBehaviorAggressive(enemy);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                Character.stateMachine.SetBehaviorIdle();
            }
        }

        public void FixedUpdate()
        {
        }

        public void CustomUpdate()
        {
            
        }
    }
}