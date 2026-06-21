using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IEnemyStates
{

    EnemyStateMachine state;

    public float iddleTimer = 10f;
    public PatrolState(EnemyStateMachine state)
    {
        this.state = state;


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Enter()
    {
        Debug.Log("voltei para o patrol state");
        state.agent.speed = state.patrolSpeed;
        //if (state.fieldOfView.fieldOfViewData.count >= state.wayPoint.Length) state.fieldOfView.fieldOfViewData.count = 0;
        // state.agent.SetDestination(state.wayPoint[state.fieldOfView.fieldOfViewData.count].position);
        state.agent.SetDestination(state.wayPoints.transform.position);

    }

    // Update is called once per frame


    public void Update()
    {
        if (state.playerInHole)
        {
            state.ChangeState(new IddleState(state));
            return;
        }
        if (state.fieldOfView.canSeePlayer)
        {
        state.ChangeState(new SeekState(state));
        return;    
        } 

        // if (Vector3.Distance(state.transform.position, state.wayPoint[state.fieldOfView.fieldOfViewData.count].position) <= 0.2f)
        if (Vector3.Distance(state.transform.position, state.wayPoints.transform.position) <= 0.5f)
        {
            //state.fieldOfView.fieldOfViewData.count++;
            //if (state.fieldOfView.fieldOfViewData.count >= state.wayPoint.Length) state.fieldOfView.fieldOfViewData.count = 0;
            //state.agent.SetDestination(state.wayPoint[state.fieldOfView.fieldOfViewData.count].position);
            state.wayPoints = state.wayPoints.next;
            state.agent.SetDestination(state.wayPoints.transform.position);
        }
       

    }

    public void Exit()
    {

    }

    public void Move()
    {

    }
}
