using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController
{
    Actor actor;
    public FSMController(Actor actor)
    {
        this.actor = actor;
    }
    IState currentState;
    public void ChangeState(IState newState)
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
