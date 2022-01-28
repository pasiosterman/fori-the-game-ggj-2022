using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ForiMovement : Mover
{
    public Transform pointA;
    public Transform pointB;
    public float maxSpeed = 5.0f;
    public float maxVelocityChange = 1.0f;

    private float movement = 0.0f;
    private Rigidbody rbody;
    
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (movement > 0.001f)
        {
            MoveTowardsTargetTransform(pointA);
        }
        else if (movement < -0.001f)
        {
            MoveTowardsTargetTransform(pointB);
        }
        else
        {
            MoveTowardsTargetTransform(null);
        }
    }

    private void MoveTowardsTargetTransform(Transform target)
    {
        if(rbody == null) return;

        Vector3 desiredVelocity = Vector3.zero;
        if(target != null){
            Vector3 dir = (target.position - transform.position).normalized;
            desiredVelocity = dir * maxSpeed;
        }

        Vector3 velocity = rbody.velocity;
        Vector3 velocityChange = desiredVelocity - velocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        rbody.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    public override void Move(Vector2 applyMovement)
    {
        movement = applyMovement.y;
    }
}
