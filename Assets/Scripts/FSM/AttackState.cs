using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Tower>
{
    public void Enter(Tower tower)
    {
        tower.anim.SetBool("IsAttack", true);
    }

    public void Exit(Tower tower)
    {
        tower.anim.SetBool("IsAttack", false);
    }

    public void Update(Tower tower)
    {
        //if(tower.towerAttackSensor.isReadyToAttack)
        //{
        //    tower.anim.SetTrigger("IsAttack");
        //}
        if (tower.detectActor.targetActor == null)
        {
            tower.fsmController.ChangeState(new IdleState());
        }
    }
}
