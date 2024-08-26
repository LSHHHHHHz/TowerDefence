using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController<T> where T : Actor
{
    private T actor;
    IState<T> currentState;
    public FSMController(T actor)
    {
        this.actor = actor;
    }
    public void ChangeState(IState<T> newState)
    {
        currentState?.Exit(actor);
        currentState = newState;
        currentState.Enter(actor);
    }
    public void FSMUpdate()
    {
        currentState.Update(actor);
    }
}
