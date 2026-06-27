using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool takePoint = false;
    void OnTriggerEnter(Collider other)
    {
        if (!takePoint)
            if (other.gameObject.CompareTag("Player"))

            {
                takePoint = true;
                GameBroadcast.CheckPointSave(transform.position);
             
            }
    }
}
