using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStats
{
    public int attackDamage { get; private set; }
    public int attackRange { get; private set; }
    public int moveSpeed { get; private set; }
  
    public ActorStats(int attackDamage, int attackRange, int moveSpeed)
    {
        this.attackDamage = attackDamage;
        this.attackRange = attackRange;
        this.moveSpeed = moveSpeed;
    }
    public void SetAttackDamage(int attackDamage)
    {
        this.attackDamage = attackDamage;
    }

    public void SetAttackRange(int attackRange)
    {
        this.attackRange = attackRange;
    }

    public void SetMoveSpeed(int moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public void IncreaseAttackDamage(int amount)
    {
        attackDamage += amount;
    }

    public void IncreaseAttackRange(int amount)
    {
        attackRange += amount;
    }

    public void IncreaseMoveSpeed(int amount)
    {
        moveSpeed += amount;
    }
}
