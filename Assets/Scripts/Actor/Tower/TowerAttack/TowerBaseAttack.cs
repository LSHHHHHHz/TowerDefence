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
    [SerializeField] float resetTime = 0.2f; // 공격 후 초기화 시간
    Transform firePos;
    private bool isReadyToAttack = false;
    private Vector3 targetPos;
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

    //이 투사체가 공유가 되니 문제가 되는 상황임
    public void FireProjectile(Vector3 firePos, Vector3 targetPos, IActor target)
    {
        BaseProjectile projectile = PoolManager.instance.GetObjectFromPool(projectilePath).GetComponent<BaseProjectile>();
        if (projectile != null)
        {
            projectile.InitializedProjectile(firePos, attackAmount, target);
            projectile.MoveTarget(targetPos, target);
        }
    }
}