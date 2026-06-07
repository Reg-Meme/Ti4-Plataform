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
        
    }

    public void Exit()
    {
        
    }

    public void Move()
    {
       
    }

    public void Update()
    {
         timer -= Time.deltaTime;
        if (timer <= 0) state.ChangeState(new PatrolState(state));
    }
}