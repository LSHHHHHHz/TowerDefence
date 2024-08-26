using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IState<Monster>
{
    public void Enter(Monster actor)
    {
        actor.anim.SetBool("IsWalk", true);
    }

    public void Exit(Monster actor)
    {
        actor.anim.SetBool("IsWalk", false);
    }

    public void Update(Monster actor)
    {
    }
}
