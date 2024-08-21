using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;

    public void InitializedPos(Vector3 firePos)
    {
        transform.position = firePos;
    }
    public abstract void MoveTarget(Vector3 dir, Vector3 targetPos);
}
