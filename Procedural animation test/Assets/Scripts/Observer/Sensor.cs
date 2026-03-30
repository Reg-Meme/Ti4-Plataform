using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    bool isActive = false;
   
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isActive)
        {
            GameBroadcast.TriggerPlayerAprouch();
            isActive = true;
            Destroy(this.gameObject);
        }
    }
}
