using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IEnemyStates
{

    EnemyStateMachine state;
    Transform[] wayPoint = new Transform[4];
    NavMeshAgent agent;
    int count = 0;
    public PatrolState(EnemyStateMachine state, Transform[] wayPoint, NavMeshAgent agent)
    {
        this.state = state;
        for (int i = 0; i < this.wayPoint.Length; i++) this.wayPoint[i] = wayPoint[i];
        this.agent = agent;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Enter()
    {

    }

    // Update is called once per frame


    public void Update()
    {
        agent.SetDestination(wayPoint[count].position);
        if (Vector3.Distance(agent.transform.position, wayPoint[count].position) <= 0.2f) 
        {
            count++;
         
        }
       // state.ChangeState(new IddleState(state))
        if (count >= wayPoint.Length ) count = 0;

    }
    public void Exit()
    {

    }

    public void Move()
    {

    }
}
