using System;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Becatled.CharacterCore.StateMachine
{
    public class CharacterBehaviorAggressive : MonoBehaviour, ICharacterBehavior
    {
        public CharacterBase characterBase { get; set; }
        public Animator _animator { get; set; }

        public void Enter(CharacterBase controller,Animator animator)
        {
            Debug.Log(("Enter aggressive behavior"));
            characterBase = controller;
            _animator = animator;
            var a = characterBase.GetClosets().transform;
            if (a != null)
                characterBase.AI.target = a;
            _animator.Play("Run");
        }

        public void Exit()
        {
            Debug.Log(("Exit aggressive behavior"));
        }

        public void Update()
        {
            var closets = characterBase.GetClosets();
            var enemyState = closets.behaviorCurrent;
            var deathState = characterBase.GetBehavior<CharacterBehaviorDeath>();
            if (closets != null && enemyState != deathState)
            {
                var dis = Vector3.Distance(characterBase.transform.position,
                    closets.transform.position);
                if (dis < characterBase._model.AttackDistance)
                    characterBase.SetBehaviorAttack();
            }
            else if (enemyState == deathState)
            {
                characterBase.SetBehaviorIdle();
            }
        }

        public void FixedUpdate()
        {
            
        }
    }
}