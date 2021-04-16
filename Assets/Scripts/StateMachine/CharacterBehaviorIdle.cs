using System;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachine
{
    public class CharacterBehaviorIdle : MonoBehaviour, ICharacterBehavior
    {
        public CharacterBase characterBase { get; set; }
        public Animator _animator { get; set; }

        public void Enter(CharacterBase characterBase,Animator animator)
        {
            Debug.Log("Enter idle behavior");
            this.characterBase = characterBase;
            _animator = animator;
            _animator.Play("Idle");
        }

        public void Exit()
        {
            Debug.Log("Exit idle behavior");
        }

        public void Update()
        {
            var closets = characterBase.GetClosets();
            if (closets != null)
            {
                var dis = Vector3.Distance(characterBase.transform.position,
                    closets.position);
                if (dis < characterBase._model.AggressiveDistance)
                {
                    if(dis < characterBase._model.AttackDistance) characterBase.SetBehaviorAttack();
                    else characterBase.SetBehaviorAggressive();
                }
            }
        }
        

        public void FixedUpdate()
        {
        }
    }
}