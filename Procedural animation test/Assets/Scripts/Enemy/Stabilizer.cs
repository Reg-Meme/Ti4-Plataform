using UnityEngine;

public class Stabilizer : MonoBehaviour
{
    Rigidbody Rig;
    public float stabilizer;
    public float Soften;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion stabilize = Quaternion.FromToRotation(transform.up, Vector3.up);
        Vector3 torque = new Vector3(stabilize.x, 0, stabilize.z) * stabilizer;
        Rig.AddTorque(torque - Rig.angularVelocity * Soften);
    }
    
}
