using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitState : IState
{
    public void Enter(Actor actor)
    {
        actor.anim.SetTrigger("GetHit");
        actor.fsmController.ChangeState(new WalkState());
    }

    public void Exit(Actor actor)
    {
        throw new System.NotImplementedException();
    }

    public void Update(Actor actor)
    {
        throw new System.NotImplementedException();
    }
}
