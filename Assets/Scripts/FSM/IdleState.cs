using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Tower>
{
    public void Enter(Tower tower)
    {
    }

    public void Exit(Tower tower)
    {      
    }

    public void Update(Tower tower)
    {   
        if(tower.detectActor != null && tower.detectActor.targetActor != null)
        {
            tower.fsmController.ChangeState(new AttackState());
        }
    }
}