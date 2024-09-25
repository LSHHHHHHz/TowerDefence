using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class TowerBaseAttack : MonoBehaviour
{
    Coroutine attackCoroutine;
    Coroutine reduceAttackDelayCoroutine;
    public float attackDelay;
    float initializedAttackDelay;
    int attackAmount;
    public string projectilePath;
    [SerializeField] float resetTime = 0.2f;
    Transform firePos;
    private bool isReadyToAttack = false;
    private Vector3 targetPos;
    public BaseProjectile projectile;
    public event Action isAttackActionFalse;
    public event Action isAttackActionTrue;

    private void Start()
    {
        projectilePath = gameObject.GetComponent<Tower>().profileDB.projectilePath;
    }
    public void Initialize(Transform firePos, int attackSpeed, int amount)
    {
        this.firePos = firePos;
        initializedAttackDelay = attackSpeed;
        attackAmount = amount;
    }
    public void StartAttack(IActor targetActor)
    {
        if (isReadyToAttack && attackCoroutine == null) 
        {
            attackCoroutine = StartCoroutine(AttackCoroutine(targetActor));
        }
    }
    public void StopAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
        if (reduceAttackDelayCoroutine == null)
        {
            reduceAttackDelayCoroutine = StartCoroutine(ReduceAttackDelay());
        }
    }
    IEnumerator ReduceAttackDelay()
    {
        while (attackDelay > 0)
        {
            attackDelay -= Time.deltaTime;
            yield return null;
        }
        attackDelay = initializedAttackDelay;
    }
    public void SetReadyToAttack(bool ready, Vector3 targetPos)
    {
        if (reduceAttackDelayCoroutine != null)
        {
            StopCoroutine(reduceAttackDelayCoroutine);
            reduceAttackDelayCoroutine = null;
        }
        isReadyToAttack = ready;
        if (ready)
        {
            this.targetPos = targetPos;
        }
    }
    IEnumerator AttackCoroutine(IActor targetActor)
    {
        isAttackActionTrue?.Invoke();
        while (targetActor != null)
        {
            attackDelay -= Time.deltaTime;
            if (attackDelay <= 0)
            {
                StartAttackAction(targetActor);
                yield return new WaitForSeconds(resetTime);
                attackDelay = initializedAttackDelay;
            }
            yield return null;
        }
        StopAttack();
    }
    void StartAttackAction(IActor target)
    {
        isAttackActionFalse?.Invoke();
        FireProjectile(firePos.position, targetPos, target);
    }
    public void FireProjectile(Vector3 firePos, Vector3 targetPos, IActor target)
    {
        BaseProjectile projectile = null;
        if (this.projectile == null)
        {
            projectile = PoolManager.instance.GetObjectFromPool(projectilePath).GetComponent<BaseProjectile>();
        }
        else
        {
            projectile = this.projectile;
        }
        if (projectile != null)
        {
            projectile.InitializedProjectile(firePos, attackAmount, target);
            projectile.MoveTarget(targetPos, target);
        }
    }
}