using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHitEffect : MonoBehaviour
{
    protected int effectStatusAmount;
    protected Action effectOnDisEnable;
    public void Initialize(Vector3 pos, int amount)
    {
        transform.position = pos;
        effectStatusAmount = amount;
    }
}
