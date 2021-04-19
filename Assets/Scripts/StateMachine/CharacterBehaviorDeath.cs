using System;
using UnityEngine;

namespace Becatled.CharacterCore.StateMachine
{
    public class CharacterBehaviorDeath : MonoBehaviour, ICharacterBehavior
    {
        public CharacterBase characterBase { get; set; }
        public Animator _animator { get; set; }
        public void Enter(CharacterBase controller, Animator animator)
        {
            characterBase = controller;
            _animator = animator;
            _animator.enabled = false;
            
            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            Material newMat = new Material(meshRenderer.sharedMaterial);
            Color newColor = meshRenderer.sharedMaterial.color;
            newColor.a = 50;
            newMat.color = newColor;
            meshRenderer.material = newMat;
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }

        public void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}