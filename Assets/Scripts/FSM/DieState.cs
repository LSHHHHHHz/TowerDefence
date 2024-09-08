using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : IState<Monster>
{
    float duration = 1;
    float elapsedTime;
    public void Enter(Monster monster)
    {
        monster.anim.SetBool("IsDie", true);
        monster.SetMonsterSpeed(0);
        elapsedTime = 0;
    }

    public void Exit(Monster monster)
    {
        throw new System.NotImplementedException();
    }

    public void Update(Monster monster)
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= duration)
        {
            monster.gameObject.SetActive(false); 
        }
    }
}
