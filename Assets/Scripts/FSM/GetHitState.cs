using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitState : IState<Monster>
{
    public void Enter(Monster actor)
    {
        actor.anim.SetTrigger("GetHit");
    }

    public void Exit(Monster actor)
    {
        throw new System.NotImplementedException();
    }

    public void Update(Monster actor)
    {
        throw new System.NotImplementedException();
    }
}
