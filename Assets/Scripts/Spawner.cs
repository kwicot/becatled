using System.Collections.Generic;
using Becatled.CharacterCore;
using DefaultNamespace;
using UnityEngine;

namespace Becatled.Spawn
{
    public class Spawner : MonoBehaviour
    {
        public GameObject Barbarian;
        public GameObject Knight;
        public GameObject Asteroid;


        public void SpawnAsteroid(Vector3 targetPos)
        {
            //TODO спавн астероида
            var direct = transform.position - targetPos;
            GameObject asteroid = Instantiate(Asteroid, targetPos + (Vector3.up * 15), Quaternion.identity);
            asteroid.GetComponent<Asteroid>().Throw(direct);

        }

        public void SpawnBarbarian(Vector3 pos)
        {
            Instantiate(Barbarian, pos, Quaternion.identity);
        }

        public void SpawnKnight(Vector3 pos)
        {
            Instantiate(Knight, pos, Quaternion.identity);
        }
    }
}