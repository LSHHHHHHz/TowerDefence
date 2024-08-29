using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Tower>
{
    public void Enter(Tower tower)
    {
        Debug.Log(tower.name + " : IdleState Enter");
    }

    public void Exit(Tower tower)
    {
        Debug.Log(tower.name + " : IdleState Exit");       
    }

    public void Update(Tower tower)
    {
        if(tower.detectActor != null && tower.detectActor.targetActor != null)
        {
            tower.fsmController.ChangeState(new AttackState());
        }
    }
}