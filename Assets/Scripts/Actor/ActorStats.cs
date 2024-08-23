using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStats
{
    public int attackDamage { get; set; }
    public int attackRange { get; set; }
    public int attackSpeed { get;  set; }
    public int moveSpeed { get;  set; }
    public int rotationSpeed {  get; set; }
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
public class MonsterStats : ActorStats
{
    public MonsterStats(int attackDamage, int attackRange, int moveSpeed, int attackSpeed, int rotationSpeed)
    {
        this.attackDamage = attackDamage;
        this.attackRange = attackRange;
        this.moveSpeed = moveSpeed;
        this.attackSpeed = attackSpeed;
        this.rotationSpeed = rotationSpeed;
    }
}
public class TowerStats : ActorStats
{
    public TowerStats(int attackDamage, int attackRange, int attackSpeed, int rotationSpeed)
    {
        this.attackDamage = attackDamage;
        this.attackRange = attackRange;
        this.attackSpeed = attackSpeed;
        this.rotationSpeed = rotationSpeed;
    }
}
