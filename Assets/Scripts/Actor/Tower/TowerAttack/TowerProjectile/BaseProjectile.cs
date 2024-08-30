using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected float projectileMoveSpeed;
    [SerializeField] protected int attackDamage;
    protected Vector3 targetPos;
    public void InitializedBullet(Vector3 firePos, int damage)
    {
        transform.position = firePos;
        attackDamage = damage;
    }
    public abstract void MoveTarget(Vector3 targetPos);
}
