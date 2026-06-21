using UnityEngine;
using UnityEngine.AI;

public class FixedIddle : IEnemyStates
{
    float timer = 5f;
    EnemyStateMachine state;

    public FixedIddle (EnemyStateMachine state)
    {
        this.state = state;
       

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
        if(!state.fixedIddle) state.ChangeState(new PatrolState(state));
    }
}