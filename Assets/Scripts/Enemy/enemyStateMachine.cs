using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateMachine 
{
    // Start is called before the first frame update
    public enemyState currentState;

    public void init(enemyState state)
    {
        currentState = state;
        currentState.Enter();
    }
    public void change(enemyState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}
