using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStatus
{
    public int currentHP { get; set;}
    public int maxHP { get; set;}
}
public class MonsterStatus : ActorStatus
{
    public MonsterStatus(int currentHP, int maxHP)
    {
        this.currentHP = currentHP;
        this.maxHP = maxHP;
    }
}
public class TowerStatus :ActorStatus
{
    public int currentExp {  get; set;}
    public int maxExp { get; private set;}
    public TowerStatus(int currentHP, int maxHP, int currentExp, int maxExp)
    {
        this.currentHP = currentHP;
        this.maxHP = maxHP;
        this.currentExp = currentExp;
        this.maxExp = maxExp;
    }
}
