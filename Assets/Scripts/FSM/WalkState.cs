using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IState
{
    public void Enter(Actor actor)
    {
        if(actor is Monster monster)
        {
            monster.anim.SetBool("IsWalk", true);
        }
    }

    public void Exit(Actor actor)
    {
        if (actor is Monster monster)
        {
            monster.anim.SetBool("IsWalk", false);
        }
    }

    public void Update(Actor actor)
    {
        if (actor is Monster monster)
        {
            monster.fsmController.ChangeState(new GetHitState());
        }
    }
}
