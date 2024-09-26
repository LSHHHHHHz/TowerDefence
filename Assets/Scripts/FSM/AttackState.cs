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
        AnimatorStateInfo stateInfo = tower.anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Attack") && stateInfo.normalizedTime >= 1.0f)
        {
            tower.fsmController.ChangeState(new IdleState());
        }
        if (tower.detectActor.targetActor == null)
        {
            tower.fsmController.ChangeState(new IdleState());
        }
    }
}
