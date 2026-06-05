using UnityEngine;

public class BreakBox : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) Destroy(this.gameObject);
    }
}
