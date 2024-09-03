using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Tower>
{
    public void Enter(Tower tower)
    {
        tower.anim.SetTrigger("IsAttack");
    }

    public void Exit(Tower tower)
    {
    }

    public void Update(Tower tower)
    {
        if (tower.detectActor.targetActor == null)
        {
            tower.fsmController.ChangeState(new IdleState());
        }
    }
}
