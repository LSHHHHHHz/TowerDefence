using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Tower>
{
    public void Enter(Tower actor)
    {
    }

    public void Exit(Tower actor)
    {
        Debug.Log(actor.name + "���� ���� (Exit)");
       
    }

    public void Update(Tower actor)
    {
    }
}