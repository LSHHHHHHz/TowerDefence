using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStatus
{
    public int currentHP { get; set;}
    public int maxHP { get; set;}   
    public int rotationSpeed { get; set; } // original speed 
    public void TakeDamage(int amount)
    {
        this.currentHP -= amount;
    }
    public void AddRotationSpeed(int amount)
    {
        this.rotationSpeed += amount;
    }    
}
public class MonsterStatus : ActorStatus
{
    public float moveSpeed { get; set; }
    public MonsterStatus(int hp, int rotationSpeed, float moveSpeed)
    {
        currentHP = hp;
        maxHP = hp;
        this.rotationSpeed = rotationSpeed;
        this.moveSpeed = moveSpeed;
    }
    public void SetMoveSpeed(float amount)
    {
        moveSpeed = amount;
    }
}
public class TowerStatus :ActorStatus
{
    public int attackStatusAmount { get; set; }
    public int attackRange { get; set; }
    public int attackSpeed { get; set; }
    public TowerStatus(int hp, int rotationSpeed, int amount, int attackRagne, int attackSpeed)
    {
        this.currentHP = hp;
        this.maxHP = hp;
        this.attackStatusAmount = amount;
        this.attackRange = attackRagne;
        this.attackSpeed = attackSpeed;
        this.rotationSpeed = rotationSpeed;
    }
    public void SetAttackDamage(int amount)
    {
        attackStatusAmount += amount;
    }

    public void SetAttackRange(int amount)
    {
        attackRange += amount;
    }
    public void SetAttackSpeed(int amount)
    {
        attackSpeed += amount;
    }
}
