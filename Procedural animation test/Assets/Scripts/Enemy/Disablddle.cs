using UnityEngine;

public class Disablddle : MonoBehaviour
{
    public EnemyStateMachine state;
    [ContextMenu("IddleOFf")]
    public void OffIddle()
    {
        state.fixedIddle = false; 
    }
    [ContextMenu("Outracoisa")]
    public void PlayerInHole()
    {
        state.playerInHole = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
        state.fixedIddle = false;
        Destroy(gameObject);
            
        } 
    }
}
