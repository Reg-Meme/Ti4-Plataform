using UnityEngine;

public class TP : MonoBehaviour
{
    public Vector3 Cord;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = Cord;
        }
    }
}
