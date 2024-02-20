using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EState
{
    Patrol,
    Chase,
    Shoot,
}

public class EnemyStateContext
{
    public IState CurrentState { get; set; }
    private readonly Enemy _controller;
    public EnemyStateContext(Enemy controller)
    {
        _controller = controller;
    }

    public void Transition(IState state)
    {
        if(CurrentState != null) CurrentState.ExitState();
        CurrentState = state;
        CurrentState.EnterState(_controller);
    }
}
