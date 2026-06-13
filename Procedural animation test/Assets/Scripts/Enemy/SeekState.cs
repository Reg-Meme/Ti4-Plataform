using UnityEngine;
using UnityEngine.AI;

public class SeekState : IEnemyStates
{

    EnemyStateMachine state;
    //  public NavMeshAgent agent;
    //  Transform[] wayPoint = new Transform[4];
    float timer = 0.2f;
    // FieldOfView fieldOfView;
    public SeekState(EnemyStateMachine state)
    {
        this.state = state;





    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Enter()
    {
        Debug.Log("State");


    }

    // Update is called once per frame


    public void Update()
    {

        if (!state.fieldOfView.canSeePlayer)
        {
            state.ChangeState(new Seeking(state));
            return;
        }
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            state.agent.SetDestination(Moviment.moviment.transform.position);
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