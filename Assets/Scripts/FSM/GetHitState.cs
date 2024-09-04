using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitState : IState<Monster>
{
    public void Enter(Monster monster)
    {
        monster.anim.SetTrigger("GetHit");
        monster.fsmController.ChangeState(new WalkState());
    }

    public void Exit(Monster monster)
    {
    }

    public void Update(Monster monster)
    {
    }
}
