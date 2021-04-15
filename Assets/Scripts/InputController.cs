using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputController : MonoBehaviour
{
    public KeyCode SpawnKnightKey = KeyCode.Alpha1;
    public KeyCode SpawnBarbarianKey = KeyCode.Alpha2;

    public KeyCode MoveLeftKey = KeyCode.A;
    public KeyCode MoveRightKey = KeyCode.D;
    public KeyCode MoveForwardKey = KeyCode.W;
    public KeyCode MoveBackwardKey = KeyCode.S;
    public MouseButton CameraMoveMouseButton = MouseButton.LeftMouse;
    private void Update()
    {
        //if(Input.GetKeyDown(SpawnKnightKey))
        //if(Input.GetKeyDown(SpawnBarbarianKey))
        
    }
}
