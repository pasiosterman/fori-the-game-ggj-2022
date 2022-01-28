using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public Vector2 Movement { get; set; }
    public Mover mover;
    
    void Update()
    {
        Movement = new Vector3(
            Input.GetAxisRaw(UnityConstants.HORIZONTAL_AXIS), 
            Input.GetAxisRaw(UnityConstants.VERTICAL_AXIS)
        );

        if(mover != null){
            mover.Move(Movement);
        }
    }
}
