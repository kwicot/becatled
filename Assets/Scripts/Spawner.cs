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
            //TODO спавн варвара
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.position = pos;
        }

        public void SpawnKnight(Vector3 pos)
        {
            //TODO спавн рыцаря
            Instantiate(Knight, pos, Quaternion.identity);
        }
    }
}