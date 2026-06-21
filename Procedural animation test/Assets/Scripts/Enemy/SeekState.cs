using UnityEngine;
using UnityEngine.AI;

public class SeekState : IEnemyStates
{

    EnemyStateMachine state;
    //  public NavMeshAgent agent;
    //  Transform[] wayPoint = new Transform[4];
    float timer = 0.2f;
    float timerSerch = 2;

    // FieldOfView fieldOfView;
    public SeekState(EnemyStateMachine state)
    {
        this.state = state;





    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Enter()
    {
        
        state.agent.speed = state.seekSpeed;

    }

    // Update is called once per frame


    public void Update()
    {
        if(state.playerInHole)
        {
        state.ChangeState(new IddleState(state));
        return;    
        } 
        if (!state.fieldOfView.canSeePlayer)
        {
            timerSerch -= Time.deltaTime;
            if (timerSerch < 0)
            {
                state.ChangeState(new Seeking(state));
                return;
            }
        }
        else timerSerch = 2;

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