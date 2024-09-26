using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    protected Monster targetMonster;
    protected float projectileMoveSpeed =10;
    protected int towerAttackmount;
    protected Vector3 targetPos;
    protected IActor target;
    public void InitializedProjectile(Vector3 firePos, int amount, IActor targetObj)
    {
        transform.position = firePos - new Vector3(0,0.5f,0);
        towerAttackmount = amount;
        target = targetObj;
    }
    public abstract void MoveTarget(Vector3 targetPos, IActor target);
}
