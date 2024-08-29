using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WalkState : IState<Monster>
{
    public void Enter(Monster monster)
    {
        Debug.Log(monster.name + " : WalkState Enter");
    }

    public void Exit(Monster monster)
    {
        Debug.Log(monster.name + " : WalkState Exit");
    }

    public void Update(Monster monster)
    {
        Debug.Log(monster.name + " : WalkState Update");
    }   
}
