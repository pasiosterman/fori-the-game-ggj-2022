using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantInput : MonoBehaviour
{
    public Vector3 input = Vector3.zero;
    public Mover mover;

    void Update()
    {
        mover.Move(input.normalized);
    }
}
