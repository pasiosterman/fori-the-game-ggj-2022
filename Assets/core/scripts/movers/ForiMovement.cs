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
    public float turnSpeed = 2.0f;

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

    // private void Update() {

    //     Vector3 dir = (pointA.position - transform.position).normalized;
    //     if (dir.magnitude > 0.0f)
    //     {
    //         transform.rotation = Quaternion.Slerp(transform.rotation,
    //             Quaternion.LookRotation(dir, Vector3.up), Time.deltaTime * turnSpeed);
    //     }
    // }

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


        // To fix any collision rotation that may happen
        Vector3 heading = (pointA.position - transform.position);
        heading.y = 0.0f;
        heading.Normalize();
        if (heading.magnitude > 0.0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(heading, Vector3.up), Time.fixedDeltaTime * turnSpeed);
        }

        rbody.angularVelocity = rbody.angularVelocity * 0.95f;
    }

    public override void Move(Vector3 applyMovement)
    {
        movement = applyMovement.z;
    }
}
