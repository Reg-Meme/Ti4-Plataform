using System;
using System.Collections;
using UnityEngine;

public class Walk : Move
{
    float time = 0.0f;
    public override void Movimentation(Vector2 input, Rigidbody rb, float maxSpeed, Transform transform)
    {
        float dotProduct = Vector3.Dot(transform.up, Vector3.up);
        if (dotProduct < 0)
        {

            return;
        }


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
        vel.x = desiredVelocity.x;
        vel.z = desiredVelocity.z;

        rb.linearVelocity = vel;
        //Debug.Log($"vel {vel.magnitude}");
        //Debug.Log($"inputMag {input.magnitude}");
        Vector3 rot = cam.transform.forward;
        rot.y = 0;
        Quaternion quat = Quaternion.LookRotation(rot);

        if (moveDir.magnitude > 0.1f)
        {
            PlayerStats.time += Time.deltaTime * 0.5f;
           transform.rotation = Quaternion.Lerp(transform.rotation, quat, PlayerStats.time);
           
        }

    }

}

