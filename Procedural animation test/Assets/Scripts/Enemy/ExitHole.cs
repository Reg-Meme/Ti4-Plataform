using UnityEngine;

public class ExitHole : MonoBehaviour
{
    public EnemyStateMachine state;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
        state.playerInHole = false;
        
            Destroy(gameObject);
        }
    }
}
