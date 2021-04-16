using System.Collections.Generic;
using Becatled.CharacterCore;
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