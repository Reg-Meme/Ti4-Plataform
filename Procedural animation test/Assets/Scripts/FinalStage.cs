using UnityEngine;

public class FinalStage : MonoBehaviour
{
    [SerializeField] Elevator elevator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        elevator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bateria"))
        {
            elevator.enabled = true;
        }
    }
}
