using UnityEngine;
using UnityEngine.AI;

public class IddleState : IEnemyStates
{
    float timer = 5f;
    EnemyStateMachine state;

    public IddleState (EnemyStateMachine state)
    {
        this.state = state;
       

    }
    public void Enter()
    {
        state.agent.SetDestination(state.hole.transform.position);
        //Toca animacao de iddle aqui
        Debug.Log("Iddle");

    }

    public void Exit()
    {
        
    }

    public void Move()
    {
       
    }

    public void Update()
    {
        if(!state.playerInHole) state.ChangeState(new SeekState(state));
    }
}