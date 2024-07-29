using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController
{
    IState currentState;
    public void ChangeState(IState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }
    public void FSMUpdate()
    {
        currentState.Update(this);
    }
}
