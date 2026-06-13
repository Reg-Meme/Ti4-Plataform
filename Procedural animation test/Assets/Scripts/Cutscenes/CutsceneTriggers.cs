using UnityEngine;

public class CutsceneTriggers : MonoBehaviour
{

    public GameObject Cut;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) Cut.SetActive(true);
        
    }
    
    
}
