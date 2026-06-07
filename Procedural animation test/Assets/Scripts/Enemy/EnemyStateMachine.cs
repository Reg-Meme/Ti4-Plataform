using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
   
    IEnemyStates state;
    public Transform[ ] wayPoint;
    public NavMeshAgent agent;
    public FieldOfView fieldOfView;
    
    
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeState(new PatrolState(this, wayPoint, agent,fieldOfView));
    }

    // Update is called once per frame
    void Update()
    {
        state?.Update();
       
    }
    public void ChangeState(IEnemyStates state)
    {
        this.state?.Exit();
        this.state = state;
        state?.Enter();
    }
    
}
