using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IEnemyStates
{

    EnemyStateMachine state;
    Transform[] wayPoint = new Transform[4];
    NavMeshAgent agent;
    FieldOfView fieldOfView;
    public float iddleTimer = 10f;
    public PatrolState(EnemyStateMachine state, Transform[] wayPoint, NavMeshAgent agent, FieldOfView fieldOfView)
    {
        this.state = state;
        for (int i = 0; i < this.wayPoint.Length; i++) this.wayPoint[i] = wayPoint[i];

        this.agent = agent;
        this.fieldOfView = fieldOfView;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Enter()
    {
        Debug.Log("voltei para o patrol state");
        if (fieldOfView.fieldOfViewData.count >= wayPoint.Length) fieldOfView.fieldOfViewData.count = 0;
        agent.SetDestination(wayPoint[fieldOfView.fieldOfViewData.count].position);

    }

    // Update is called once per frame


    public void Update()
    {
        if (fieldOfView.fieldOfViewData.canSeePlayer) state.ChangeState(new SeekState(state, agent, wayPoint, fieldOfView));

        if (Vector3.Distance(agent.transform.position, wayPoint[fieldOfView.fieldOfViewData.count].position) <= 0.2f)
        {
            fieldOfView.fieldOfViewData.count++;
            if (fieldOfView.fieldOfViewData.count >= wayPoint.Length) fieldOfView.fieldOfViewData.count = 0;
            agent.SetDestination(wayPoint[fieldOfView.fieldOfViewData.count].position);
        }
        iddleTimer -= Time.deltaTime;
        if (iddleTimer <= 0)
        {
            state.ChangeState(new IddleState(state, agent, fieldOfView, wayPoint));
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
