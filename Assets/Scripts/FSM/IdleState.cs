using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void Enter(Actor actor)
    {
        Debug.Log(actor.name + "에 진입 (Enter)");
        if (actor is Tower tower)
        {
            tower.anim.SetBool("IsAttack", false);
        }
        if(actor is Monster monster)
        {

        }
    }

    public void Exit(Actor actor)
    {
        Debug.Log(actor.name + "에서 나옴 (Exit)");
        if (actor is Tower tower)
        {

        }
        if (actor is Monster monster)
        {

        }
    }

    public void Update(Actor actor)
    {
        Debug.Log(actor.name + " 업데이트");
        if (actor is Tower tower)
        {
            if(tower.towerAttackSensor.findActor)
            {
                tower.fsmController.ChangeState(new AttackState());
            }
        }
        if (actor is Monster monster)
        {

        }
    }
}
