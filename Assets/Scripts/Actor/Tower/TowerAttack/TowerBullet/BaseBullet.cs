using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    public abstract void MoveTarget(Vector3 dir, Vector3 targetPos);
}
