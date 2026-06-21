using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTriggers : MonoBehaviour
{

    public GameObject Cut;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            Cut.SetActive(true);
            
        }
        
    }
    
    
}
