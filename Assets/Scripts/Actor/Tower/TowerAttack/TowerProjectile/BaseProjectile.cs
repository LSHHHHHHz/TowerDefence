using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected float projectileMoveSpeed;
    [SerializeField] protected int towerAttackmount;
    protected Vector3 targetPos;
    public void InitializedBullet(Vector3 firePos, int amount)
    {
        transform.position = firePos;
        towerAttackmount = amount;
    }
    public abstract void MoveTarget(Vector3 targetPos);
}
