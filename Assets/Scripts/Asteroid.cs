using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Becatled.CharacterCore;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class Asteroid : MonoBehaviour
    {
        public float KillRadius;
        public float Speed;
        public ParticleSystem ExplosionParticle;


        private Rigidbody _rigidbody;


        public void Throw(Vector3 direct)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = direct * Speed;
        }

        void Explosion()
        {
            /*
             * Поиск врагов входящих в убойный радиус
             * С последующим толчком(Взрывная волна)
             * И Убийством
             */
            Instantiate(ExplosionParticle, transform.position, quaternion.identity).Play();

            CharacterBase[] characters = FindObjectsOfType<CharacterBase>();
            foreach (var character in characters)
            {
                if (character != null)
                {
                    var dis = Vector3.Distance(transform.position, character.transform.position);
                    if (dis < KillRadius)
                    {
                        character.MakeDamage(10000, false);
                        
                        Rigidbody[] _rigidbodies = character.gameObject.GetComponentsInChildren<Rigidbody>();
                        foreach (var _rb in _rigidbodies)
                        {
                            var dir = (_rb.position - transform.position) * 400;
                            _rb.AddForce(dir);
                        }
                    }
                }
            }
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            Explosion();
        }
    }
}