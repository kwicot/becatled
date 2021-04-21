using System;
using System.Collections.Generic;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachineCore
{
    public class StateMachine 
    {
        protected Dictionary<Type, ICharacterBehavior> behaviorsMap;
        private Animator _characterAnimator;
        private CharacterBase _character;

        public ICharacterBehavior behaviorCurrent { get; protected set; }

            

        public void Update()
        {
            if (behaviorCurrent != null) behaviorCurrent.Update();
        }
        public void FixedUpdate()
        {
            if(behaviorCurrent != null) behaviorCurrent.FixedUpdate();
        }
        
        
        protected void InitBehaviors()
        {
            behaviorsMap = new Dictionary<Type, ICharacterBehavior>();
            
            behaviorsMap[typeof(CharacterBehaviorIdle)] = new CharacterBehaviorIdle();
            behaviorsMap[typeof(CharacterBehaviorWait)] = new CharacterBehaviorWait();
            behaviorsMap[typeof(CharacterBehaviorAttack)] = new CharacterBehaviorAttack();
            behaviorsMap[typeof(CharacterBehaviorAggressive)] = new CharacterBehaviorAggressive();
        }
        protected void SetBehavior(ICharacterBehavior behavior)
        {
            if (behaviorCurrent != null)
                behaviorCurrent.Exit();

            _character.SelectedEnemy = null;
            behaviorCurrent = behavior;
            behaviorCurrent.Enter(_character);
        }
        protected void SetBehavior(ICharacterBehavior behavior,CharacterBase enemy)
        {
            if (behaviorCurrent != null)
                behaviorCurrent.Exit();

            _character.SelectedEnemy = enemy;
            behaviorCurrent = behavior;
            behaviorCurrent.Enter(_character,enemy);
        }
        public ICharacterBehavior GetBehavior<T>() where T : ICharacterBehavior
        {
            var type = typeof(T);
            return behaviorsMap[type];
        }
        
        
        public void SetBehaviorIdle()
        {
            var behavior = GetBehavior<CharacterBehaviorIdle>();
            SetBehavior(behavior);
        }
        public void SetBehaviorIdle(CharacterBase enemy)
        {
            var behavior = GetBehavior<CharacterBehaviorIdle>();
            SetBehavior(behavior,enemy);
        }

        public void SetBehaviorAggressive()
        {
            var behavior = GetBehavior<CharacterBehaviorAggressive>();
            SetBehavior(behavior);
        }
        public void SetBehaviorAggressive(CharacterBase enemy)
        {
            var behavior = GetBehavior<CharacterBehaviorAggressive>();
            SetBehavior(behavior,enemy);
        }

        public void SetBehaviorWait()
        {
            var behavior = GetBehavior<CharacterBehaviorWait>();
            SetBehavior(behavior);
        }
        public void SetBehaviorWait(CharacterBase enemy)
        {
            var behavior = GetBehavior<CharacterBehaviorWait>();
            SetBehavior(behavior,enemy);
        }

        public void SetBehaviorAttack()
        {
            var behavior = GetBehavior<CharacterBehaviorAttack>();
            SetBehavior(behavior);
        }
        public void SetBehaviorAttack(CharacterBase enemy)
        {
            var behavior = GetBehavior<CharacterBehaviorAttack>();
            SetBehavior(behavior,enemy);
        }


        public StateMachine(CharacterBase character,Animator animator)
        {
            _character = character;
            _characterAnimator = animator;
            
            InitBehaviors();
            SetBehaviorIdle();
        }
    }
}