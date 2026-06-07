using UnityEngine;
using UnityEngine.AI;

public class IddleState : IEnemyStates
{
    float timer = 5f;
    EnemyStateMachine state;
    NavMeshAgent agent;
    FieldOfView fieldOfView;
    Transform[] wayPoint = new Transform[4];
    public IddleState (EnemyStateMachine state, NavMeshAgent agent, FieldOfView fieldOfView, Transform[] wayPoint)
    {
        this.state = state;
        this.agent = agent;
        this.fieldOfView = fieldOfView;
        this.wayPoint = wayPoint;

    }
    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void Move()
    {
       
    }

    public void Update()
    {
         timer -= Time.deltaTime;
        if (timer <= 0) state.ChangeState(new PatrolState(state,wayPoint,agent,fieldOfView));
    }
}