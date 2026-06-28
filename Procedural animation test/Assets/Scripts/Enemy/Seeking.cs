

using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class Seeking : IEnemyStates
{

    EnemyStateMachine state;

    //NavMeshAgent agent;

    int count = 0;
    // FieldOfView fieldOfView;
    // Transform[] wayPoint = new Transform[4];
    Vector3[] randomPos = new Vector3[2];
    public Seeking(EnemyStateMachine state)
    {
        this.state = state;
       

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Enter()
    {
       
        randomPos[0] = state.fieldOfView.fieldOfViewData.pos.position + (Random.insideUnitSphere * state.fieldOfView.fieldOfViewData.radius);
        randomPos[1] = state.fieldOfView.fieldOfViewData.pos.position + (Random.insideUnitSphere * state.fieldOfView.fieldOfViewData.radius);
        randomPos[0].y = state.transform.position.y;
        randomPos[1].y = state.transform.position.y;
        state.agent.SetDestination(randomPos[0]);
        state.agent.speed = state.patrolSpeed;
    }

    // Update is called once per frame


    public void Update()
    {
        if(state.fieldOfView.canSeePlayer)
        {
            state.ChangeState(new SeekState(state));
            return;
        }
      //  Debug.Log(!state.agent.pathPending && state.agent.remainingDistance <= 0.7f);
        if (!state.agent.pathPending && state.agent.remainingDistance <= 0.7f)
        {   

            count++;
            if (count >= 2)
            {
                Debug.LogError("to entrando aqui ");
                state.ChangeState(new PatrolState(state));
                return;
            }
           state.agent.SetDestination(randomPos[count]);
         
            Debug.LogError("count: " + count );

        }



    }
    public void Exit()
    {

    }

    public void Move()
    {

    }
}