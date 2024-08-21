using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void Enter(Actor actor)
    {
        Debug.Log(actor.name + " Attack Enter");
        if (actor is Tower tower)
        {
            tower.anim.SetBool("IsAttack", true);
        }
    }

    public void Exit(Actor actor)
    {
        Debug.Log(actor.name + " Attack Exit");
        if (actor is Tower tower)
        {
            tower.anim.SetBool("IsAttack", false);
        }
    }

    public void Update(Actor actor)
    {
        Debug.Log(actor.name + " Attack 업데이트");
        if (actor is Tower tower)
        {
            if(!tower.towerAttackSensor.findActor)
            {
                tower.fsmController.ChangeState(new IdleState());
            }
        }
    }
}
