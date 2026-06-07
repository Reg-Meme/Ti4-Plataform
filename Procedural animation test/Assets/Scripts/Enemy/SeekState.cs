using UnityEngine;
using UnityEngine.AI;

public class SeekState : IEnemyStates
{
    
    EnemyStateMachine state;
     public NavMeshAgent agent;
     Transform[] wayPoint = new Transform[4];
     float timer = 0.2f;
     FieldOfView fieldOfView;
    public SeekState(EnemyStateMachine state, NavMeshAgent agent, Transform[] wayPoint, FieldOfView fieldOfView)
    {
        this.state = state;
         for (int i = 0; i < this.wayPoint.Length; i++) this.wayPoint[i] = wayPoint[i];
        
        this.agent = agent;
        this.fieldOfView = fieldOfView;
    

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Enter()
    {
       Debug.Log("State"); 
        
       
    }

    // Update is called once per frame
    

    public void Update()
    {
     
       if(!fieldOfView.fieldOfViewData.canSeePlayer)
        {
        state.ChangeState(new Seeking(state, agent,fieldOfView, wayPoint));
        return;    
        } 
       timer -= Time.deltaTime;
       if (timer <= 0)
        {
        agent.SetDestination(Moviment.moviment.transform.position);
        timer = 0;    
        } 

    }
    public void Exit()
    {
        
    }

    public void Move()
    {
       
    }
}