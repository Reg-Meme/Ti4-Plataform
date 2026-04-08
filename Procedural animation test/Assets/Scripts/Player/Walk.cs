using UnityEngine;

public class Walk : Move
{
    public override void Movimentation(Vector2 input, Rigidbody rb, float maxSpeed)
    {
 
        Transform cam = Camera.main.transform;

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * input.y + right * input.x;

        if (moveDir.magnitude > 1f) moveDir.Normalize();

        Vector3 vel = rb.linearVelocity;
        Vector3 desiredVelocity = moveDir * maxSpeed;
        //Vector3 velocityChange = desiredVelocity - horizontalVel;
        vel.x = desiredVelocity.x;
        vel.z = desiredVelocity.z;
        rb.linearVelocity = vel;
        //Rig.AddForce(velocityChange * acceleration, ForceMode.Acceleration);
        
    }
}

