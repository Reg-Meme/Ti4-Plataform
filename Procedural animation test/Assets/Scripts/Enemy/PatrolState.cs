using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.AdaptivePerformance.Editor;
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
        if (state.fieldOfView.fieldOfViewData.count >= state.wayPoint.Length) state.fieldOfView.fieldOfViewData.count = 0;
        state.agent.SetDestination(state.wayPoint[state.fieldOfView.fieldOfViewData.count].position);

    }

    // Update is called once per frame


    public void Update()
    {
        if (state.fieldOfView.fieldOfViewData.canSeePlayer) state.ChangeState(new SeekState(state));

        if (Vector3.Distance(state.transform.position, state.wayPoint[state.fieldOfView.fieldOfViewData.count].position) <= 0.2f)
        {
            state.fieldOfView.fieldOfViewData.count++;
            if (state.fieldOfView.fieldOfViewData.count >= state.wayPoint.Length) state.fieldOfView.fieldOfViewData.count = 0;
            state.agent.SetDestination(state.wayPoint[state.fieldOfView.fieldOfViewData.count].position);
        }
        iddleTimer -= Time.deltaTime;
        if (iddleTimer <= 0)
        {
            state.ChangeState(new IddleState(state));
            return;
        }


    }

    public void Exit()
    {

    }

    public void Move()
    {

    }
}
