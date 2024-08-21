using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected int bulletDamage;
    protected Vector3 targetPos;
    public void InitializedBullet(Vector3 firePos, int damage)
    {
        transform.position = firePos;
        bulletDamage = damage;
    }
    public abstract void MoveTarget(Vector3 targetPos);
}
