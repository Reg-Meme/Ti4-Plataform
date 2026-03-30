using UnityEngine;

public class CheckPoint : MonoBehaviour
{
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            GameBroadcast.CheckPointSave(transform.position);
        
    }

}
