using UnityEngine;

public class Disablddle : MonoBehaviour
{
    public EnemyStateMachine state;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
        state.fixedIddle = false;
        Destroy(gameObject);
            
        } 
    }
}
