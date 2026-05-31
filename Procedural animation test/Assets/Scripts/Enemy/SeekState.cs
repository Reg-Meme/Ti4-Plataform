using UnityEngine;
using UnityEngine.AI;

public class SeekState : IEnemyStates
{
    
    EnemyStateMachine state;
     public NavMeshAgent agent;
     Transform[] wayPoint = new Transform[4];
    public SeekState(EnemyStateMachine state, NavMeshAgent agent, Transform[] wayPoint)
    {
        this.state = state;
         for (int i = 0; i < this.wayPoint.Length; i++) this.wayPoint[i] = wayPoint[i];
        this.agent = agent;


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Enter()
    {
       Debug.Log("State");
    }

    // Update is called once per frame
    

    public void Update()
    {
       agent.SetDestination(Moviment.moviment.transform.position);
       if(!FieldOfView.fieldOfView.canSeePlayer) state.ChangeState(new PatrolState(state, wayPoint, agent));
    }
    public void Exit()
    {
        
    }

    public void Move()
    {
       
    }
}