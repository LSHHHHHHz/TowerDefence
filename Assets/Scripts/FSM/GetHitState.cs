using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitState : IState<Monster>
{
    public void Enter(Monster monster)
    {
        Debug.LogError(monster.name + " : GetHitState Enter");
        monster.anim.SetTrigger("GetHit");
        monster.fsmController.ChangeState(new WalkState());
    }

    public void Exit(Monster monster)
    {
        Debug.Log(monster.name + " : GetHitState Exit");
    }

    public void Update(Monster monster)
    {
        Debug.Log(monster.name + " : GetHitState Update");
    }
}
