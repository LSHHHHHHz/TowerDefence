using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController
{
    Actor actor;
    IState currentState;
    public FSMController(Actor actor)
    {
        this.actor = actor;
    }
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
