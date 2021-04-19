using System;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachineCore
{
    public class CharacterBehaviorDeath : MonoBehaviour, ICharacterBehavior
    {
        public CharacterBase Character { get; set; }
        public void Enter(CharacterBase controller,CharacterBase _enemy = null)
        {
            Character = controller;
            Character._animator.enabled = false;

            SkinnedMeshRenderer meshRenderer = Character.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            Material newMat = new Material(meshRenderer.material);
            meshRenderer.material = newMat;
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            Debug.Log("DeathUpdate");
            Character.transform.Translate(Vector3.down);
        }

        public void FixedUpdate()
        {
        }
    }
}