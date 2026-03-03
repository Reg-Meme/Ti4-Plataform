using UnityEngine;

public class gravityTest : MonoBehaviour
{
    public float gravityForce = 9.81f;
    public float gravityScale = 1;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector3(0,-gravityScale*gravityForce,0), ForceMode.Acceleration);
    }

}
