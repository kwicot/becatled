using System;
using System.Collections;
using System.Collections.Generic;
using Becatled.CharacterCore.StateMachine;
using UnityEngine;

namespace Becatled.CharacterCore
{
    [RequireComponent(typeof(Animator))]
    public class CharacterBase : MonoBehaviour
    {
        protected Dictionary<Type, ICharacterBehavior> behaviorsMap;
        protected ICharacterBehavior behaviorCurrent;

        protected Character_Model _model;
        protected Animator _animator;

        void Start()
        {
            InitBehaviors();
            SetBehaviorIdle();
        }

        void Update()
        {
            if (behaviorCurrent != null) behaviorCurrent.Update();
        }

        private void FixedUpdate()
        {
            if(behaviorCurrent != null) behaviorCurrent.FixedUpdate();
        }

        protected virtual void Init()
        {
            
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

            behaviorCurrent = behavior;
            behaviorCurrent.Enter(this,_animator);
        }
        protected ICharacterBehavior GetBehavior<T>() where T : ICharacterBehavior
        {
            var type = typeof(T);
            return behaviorsMap[type];
        }

        

        public void SetBehaviorIdle()
        {
            var behavior = GetBehavior<CharacterBehaviorIdle>();
            SetBehavior(behavior);
        }

        public void SetBehaviorAggressive()
        {
            var behavior = GetBehavior<CharacterBehaviorAggressive>();
            SetBehavior(behavior);
        }

        public void SetBehaviorWait()
        {
            var behavior = GetBehavior<CharacterBehaviorWait>();
            SetBehavior(behavior);
        }

        public void SetBehaviorAttack()
        {
            var behavior = GetBehavior<CharacterBehaviorAttack>();
            SetBehavior(behavior);
        }
        
    }
}
