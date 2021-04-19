using System;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Becatled.CharacterCore.StateMachineCore
{
    public class CharacterBehaviorAggressive : MonoBehaviour, ICharacterBehavior
    {
        public CharacterBase Character { get; set; }
        private CharacterBase enemy;


        private ICharacterBehavior deathState;
        private ICharacterBehavior enemyState;

        public void Enter(CharacterBase controller,CharacterBase _enemy = null)
        {
            //Debug.Log(("Enter aggressive behavior"));
            Character = controller;
            enemy = _enemy;
            if (enemy != null)
            {
                Character.AI.target = enemy.transform;
                Character._animator.Play("Run");
            }
            else
                Character.stateMachine.SetBehaviorIdle();

            deathState = Character.stateMachine.GetBehavior<CharacterBehaviorDeath>();
            enemyState  = enemy.stateMachine.behaviorCurrent;
        }
        

        public void Exit()
        {
            //Debug.Log(("Exit aggressive behavior"));
        }

        public void Update()
        {
            if (enemy != null && enemyState != deathState)
            {
                var dis = Vector3.Distance(Character.transform.position,
                    enemy.transform.position);
                if (dis < Character._model.AttackDistance)
                    Character.stateMachine.SetBehaviorAttack(enemy);
            }
            else if (enemyState == deathState | enemy == null)
            {
                Character.stateMachine.SetBehaviorIdle();
                Character.SelectedEnemy = null;
            }
        }

        public void FixedUpdate()
        {
            
        }
    }
}