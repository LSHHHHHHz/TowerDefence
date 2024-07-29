using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStatus
{
    public int currentHP { get; set;}
    public int maxHP { get; private set;}
    public int currentMP { get; set;}
    public int maxMP { get; private set;}
    public ActorStatus(int maxHP, int maxMP)
    {
        this.maxHP = maxHP;
        this.currentHP = maxHP;
        this.maxMP = maxMP;
        this.currentMP = maxMP;
    }
}
