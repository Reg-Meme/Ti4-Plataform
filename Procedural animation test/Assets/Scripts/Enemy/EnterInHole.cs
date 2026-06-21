using UnityEngine;

public class EnterInHole : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     public EnemyStateMachine state;
     public WayPoint wayPoint;
     public GameObject triggerOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerOn.SetActive(false);
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
        state.playerInHole = true;
        state.hole = wayPoint;
        triggerOn.SetActive(true);
            Destroy(gameObject);
        }
    }
}
