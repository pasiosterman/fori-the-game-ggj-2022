using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ActorMover : Mover
{
    public float maxSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    public Vector3 MoveDir { get; private set; }
    Rigidbody rbody;

    Transform mountedTo;


    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 desiredVel = new Vector3(MoveDir.x, 0.0f, MoveDir.z);
        desiredVel = desiredVel * maxSpeed;
        rbody.velocity = new Vector3(desiredVel.x, rbody.velocity.y, desiredVel.z);

        rbody.angularVelocity = rbody.angularVelocity * 0.95f;

        if (MoveDir.magnitude > 0.0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(MoveDir, Vector3.up), Time.fixedDeltaTime * turnSpeed);
        }
    }

    public override void Move(Vector3 applyMovement)
    {
        MoveDir = new Vector3(applyMovement.x, 0, applyMovement.z);
    }
}
