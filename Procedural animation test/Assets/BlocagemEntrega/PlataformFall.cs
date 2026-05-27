using UnityEngine;

public class PlataformFall : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(transform.parent == null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}
