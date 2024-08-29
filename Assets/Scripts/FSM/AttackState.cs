using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Tower>
{
    public void Enter(Tower tower)
    {
        Debug.Log(tower.name + " Attack Enter");
    }

    public void Exit(Tower tower)
    {
        Debug.Log(tower.name + " Attack Exit");
        tower.anim.SetBool("IsAttack", false);
    }

    public void Update(Tower tower)
    {
        Debug.Log(tower.name + " Attack Update");
        if(tower.towerAttackSensor.isReadyToAttack && tower.towerAttackSensor.towerBaseAttack.isAttackAction)
        {
            tower.anim.SetBool("IsAttack", true);
        }
        else
        {
            tower.anim.SetBool("IsAttack", false);
        }
    }
}
