using UnityEngine;

public class Roll : Move
{
    Transform body;
    LayerMask layerMask;
    Transform transform;
    public Roll (Transform body, LayerMask layerMask, Transform transform)
    {
        this.body = body;
        this.layerMask = layerMask;
        this.transform = transform;
    }

    public override void Movimentation(Vector2 input, Rigidbody rb, float maxSpeed)
    {
         bool IsSided = Physics.Raycast(rb.position, Vector3.down, .5f, layerMask);
        Transform cam = Camera.main.transform;
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 rollDir = (right * input.y) + (forward * -input.x);// Eixo invertido para girar certo 
        if (IsSided)
        {
            rb.AddForce(Vector3.down * 500, ForceMode.Force);
            rb.AddForce(Vector3.Cross(rollDir, Vector3.up) * 100);

            if (rb.angularVelocity.magnitude < maxSpeed)
            {
                Vector3 rotationForce = Vector3.Cross(transform.up, Vector3.up) * -input.x * 100;
                rb.AddForceAtPosition(rotationForce, transform.position + transform.up);
            }
        }
      
    //    bool IsSided = Physics.Raycast(body.position, Vector3.down, .5f, layerMask);
      
    //     Transform cam = Camera.main.transform;
    //     Vector3 forward = cam.forward;
    //     Vector3 right = cam.right;

    //     forward.y = 0;
    //     right.y = 0;
    //     forward.Normalize();
    //     right.Normalize();

    //     Vector3 rollDir = (right * input.y) + (forward * -input.x);// Eixo invertido para girar certo 
    //      if (IsSided)
    //     {
    //         rb.AddForce(Vector3.down * 500, ForceMode.Force);
    //         rb.AddForce(Vector3.Cross(rollDir, Vector3.up) * 100);

    //         if (rb.angularVelocity.magnitude < maxSpeed)
    //         {
    //             Vector3 rotationForce = Vector3.Cross(transform.up, Vector3.up) * input.x * 100;
    //             rb.AddForceAtPosition(rotationForce, transform.position + transform.up);
    //         }
    //     }
    }
   

    
  
}
