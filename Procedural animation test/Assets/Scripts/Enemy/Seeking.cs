

using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class Seeking : IEnemyStates
{

    EnemyStateMachine state;

    NavMeshAgent agent;

    int count = 0;
    FieldOfView fieldOfView;
    Transform[] wayPoint = new Transform[4];
    Vector3[] randomPos = new Vector3[2];
    public Seeking(EnemyStateMachine state, NavMeshAgent agent, FieldOfView fieldOfView, Transform[] wayPoint)
    {
        this.state = state;
        this.agent = agent;
        this.fieldOfView = fieldOfView;
        for (int i = 0; i < this.wayPoint.Length; i++) this.wayPoint[i] = wayPoint[i];

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Enter()
    {
        Debug.Log("here");
        randomPos[0] = fieldOfView.fieldOfViewData.pos.position + (Random.insideUnitSphere * fieldOfView.fieldOfViewData.radius);
        randomPos[1] = fieldOfView.fieldOfViewData.pos.position + (Random.insideUnitSphere * fieldOfView.fieldOfViewData.radius);
        randomPos[0].y = agent.transform.position.y;
        randomPos[1].y = agent.transform.position.y;
        agent.SetDestination(randomPos[0]);
    }

    // Update is called once per frame


    public void Update()
    {
        if(fieldOfView.fieldOfViewData.canSeePlayer)
        {
            state.ChangeState(new SeekState(state, agent, wayPoint, fieldOfView));
            return;
        }

        if (Vector3.Distance(agent.transform.position, randomPos[count]) <= 0.3f)
        {
            Debug.Log("" + count + "");
            count++;
            if (count >= 2)
            {
                state.ChangeState(new PatrolState(state, wayPoint, agent, fieldOfView));
                return;
            }
            agent.SetDestination(randomPos[count]);


        }



    }
    public void Exit()
    {

    }

    public void Move()
    {

    }
}