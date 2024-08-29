using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitState : IState<Monster>
{
    public void Enter(Monster monster)
    {
        Debug.Log(monster.name + " : GetHitState Enter");
        monster.anim.SetTrigger("GetHit");
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
