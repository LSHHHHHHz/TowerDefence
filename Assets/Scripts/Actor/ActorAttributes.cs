using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAttributes
{
    public int currentHP { get; set;}
    public int maxHP { get; set;}   
    public int rotationSpeed { get; set; }
    public void TakeDamage(int amount)
    {
        this.currentHP -= amount;
    }
    public void AddRotationSpeed(int amount)
    {
        this.rotationSpeed += amount;
    }    
}
public class MonsterAttributes : ActorAttributes
{
    public float moveSpeed { get; private set; }
    public float originSpeed { get; private set; }

    public MonsterAttributes(int hp, int rotationSpeed, float moveSpeed)
    {
        currentHP = hp;
        maxHP = hp;
        this.rotationSpeed = rotationSpeed;
        this.moveSpeed = moveSpeed;
        this.originSpeed = moveSpeed; 
    }
    public void SetMoveSpeed(float amount)
    {
        moveSpeed = amount;
    }
    public void ResetMoveSpeed()
    {
        moveSpeed = originSpeed;
    }
}
public class TowerAttributes :ActorAttributes
{
    public int attackStatusAmount { get; set; }
    public int attackRange { get; set; }
    public int attackSpeed { get; set; }
    public TowerAttributes(int hp, int rotationSpeed, int amount, int attackRagne, int attackSpeed)
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
