using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void Enter(Actor actor)
    {
        Debug.Log(actor.name + "�� ���� (Enter)");
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
        Debug.Log(actor.name + "���� ���� (Exit)");
        if (actor is Tower tower)
        {

        }
        if (actor is Monster monster)
        {

        }
    }

    public void Update(Actor actor)
    {
        Debug.Log(actor.name + " ������Ʈ");
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
