using System;
using UnityEngine;
using Becatled.Spawn;

namespace Becatled.Controll
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        public float Speed;
        public Spawner spawner;
        public LayerMask LayerMask;


        private Camera _camera;
        private GameObject pointer;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pointer.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
            pointer.layer = 2;
        }

        private void Update()
        {
            InputUpdate();
            MoveUpdate();
        }

        private void InputUpdate()
        {
            if(Input.GetMouseButton(1)) RotationUpdate();
            
            
            
            Vector3 mousePos = Vector3.zero;
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit,1000,LayerMask))
            {
                mousePos = hit.point;
                pointer.transform.position= mousePos;

            }
            else return;
            if(Input.GetKeyDown(KeyCode.X)) spawner.SpawnKnight(mousePos); 
            if(Input.GetKeyDown(KeyCode.Z)) spawner.SpawnBarbarian(mousePos);
            if(Input.GetMouseButtonDown(0)) spawner.SpawnAsteroid(mousePos);



            if (Input.GetKeyDown(KeyCode.Alpha1)) Time.timeScale = 0.1f;
            if (Input.GetKeyDown(KeyCode.Alpha2)) Time.timeScale = 1f;
        }

        void MoveUpdate()
        {
            float x = Input.GetAxis("X");
            float y = Input.GetAxis("Y");
            float z = Input.GetAxis("Z") / 2;
            Vector3 dir = new Vector3(x, y, z);
            transform.Translate(dir * Speed);
        }

        private float cameraHorizontal;
        private float cameraVertical;
        void RotationUpdate()
        {
            cameraHorizontal += Input.GetAxis("Mouse X") * 2;
            cameraVertical -= Input.GetAxis("Mouse Y") * 2;
            transform.rotation = Quaternion.Euler(cameraVertical, cameraHorizontal, 0);
        }
    }
}