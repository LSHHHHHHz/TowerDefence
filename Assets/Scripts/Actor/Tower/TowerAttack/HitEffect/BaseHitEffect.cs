using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHitEffect : MonoBehaviour
{
    protected Vector3 originPos;
    protected int combatEffectAmount;
    protected Action effectOnDisEnable;
    protected virtual void Awake()
    {
        originPos = transform.position;
    }
    public void Initialize(Vector3 pos, int amount)
    {
        transform.position = new Vector3(pos.x, originPos.y, pos.z);
        combatEffectAmount = amount;
    }
}
