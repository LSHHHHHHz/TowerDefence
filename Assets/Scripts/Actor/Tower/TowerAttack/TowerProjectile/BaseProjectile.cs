using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    protected float projectileMoveSpeed =10;
    protected int towerAttackmount;
    protected Vector3 targetPos;
    public void InitializedBullet(Vector3 firePos, int amount)
    {
        transform.position = firePos - new Vector3(0,0.5f,0);
        towerAttackmount = amount;
    }
    public abstract void MoveTarget(Vector3 targetPos);
}
