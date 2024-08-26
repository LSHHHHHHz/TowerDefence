using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Tower>
{
    public void Enter(Tower actor)
    {
        Debug.Log(actor.name + " Attack Enter");
    }

    public void Exit(Tower actor)
    {
        Debug.Log(actor.name + " Attack Exit");
    }

    public void Update(Tower actor)
    {
        Debug.Log(actor.name + " Attack 업데이트");
    }
}
