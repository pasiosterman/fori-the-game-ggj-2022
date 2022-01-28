using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ForiMovement : Mover
{
    public Transform pointA;
    public Transform pointB;

    public float maxSpeed = 5.0f;

    private Rigidbody rbody;
    private float movement;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        
        if(movement > 0.0f){
            MoveTowardsTargetTransform(pointA);
        }
        else if(movement < 0.0f){
            MoveTowardsTargetTransform(pointB);
        }
    }

    private void MoveTowardsTargetTransform(Transform target){

        Vector3 dir =  (target.position - transform.position).normalized;
        rbody.velocity = dir * maxSpeed;
    }

    public override void Move(Vector2 applyMovement)
    {
        movement = applyMovement.y;
    }
}
