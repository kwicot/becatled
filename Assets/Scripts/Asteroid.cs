using System;
using System.Xml.Serialization;
using Becatled.CharacterCore;
using UnityEngine;

namespace DefaultNamespace
{
    public class Asteroid : MonoBehaviour
    {
        public float KillRadius;
        public float Speed;


        private Rigidbody _rigidbody;
        private CapsuleCollider trigger;


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            trigger = gameObject.AddComponent<CapsuleCollider>();
            trigger.isTrigger = true;
        }

        public void Throw(Vector3 direct)
        {
            _rigidbody.velocity = direct * Speed;
        }

        private void OnCollisionEnter(Collision other)
        {
            trigger.radius = KillRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out CharacterBase character))
            {
                character.MakeDamage(10000);
            }
        }
    }
}