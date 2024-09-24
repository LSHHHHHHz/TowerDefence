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
        monster.anim.SetBool("IsDie", false);
    }

    public void Update(Monster monster)
    {
        Debug.LogError(elapsedTime);
        elapsedTime += Time.deltaTime;
        monster.SetMonsterSpeed(0);
        if (elapsedTime >= duration)
        {
            monster.gameObject.SetActive(false); 
        }
    }
}
