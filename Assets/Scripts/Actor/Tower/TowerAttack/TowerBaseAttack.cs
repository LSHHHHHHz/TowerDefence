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
    int attackDamage;
    public string projectilePath;
    [SerializeField] float resetTime = 0.2f; // 공격 후 초기화 시간
    public bool isAttackAction; //애니메이션할때 필요
    Transform firePos;
    private bool isReadyToAttack = false;
    private Vector3 targetPos;
    private void Start()
    {
        projectilePath = gameObject.GetComponent<Tower>().profileDB.projectilePath;
    }
    public void Initialize(Transform firePos, int attackSpeed, int damage)
    {
        this.firePos = firePos;
        initializedAttackDelay = attackSpeed;
        attackDamage = damage;
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
        isAttackAction = false; 
    }
    IEnumerator ReduceAttackDelay()
    {
        while (isAttackAction == false && attackDelay > 0)
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
        while (targetActor != null)
        {
            attackDelay -= Time.deltaTime;
            if (attackDelay <= 0)
            {
                StartAttackAction(); 
                yield return new WaitForSeconds(resetTime); 
                isAttackAction = false; 
                attackDelay = initializedAttackDelay; 
            }
            yield return null;
        }
        StopAttack(); 
    }
    void StartAttackAction()
    {
        isAttackAction = true; 
        FireProjectile(firePos.position, targetPos);
    }
    public void FireProjectile(Vector3 firePos, Vector3 targetPos)
    {
        BaseProjectile projectile = PoolManager.instance.GetObjectFromPool(projectilePath).GetComponent<BaseProjectile>();
        if (projectile != null)
        {
            projectile.InitializedBullet(firePos, attackDamage);
            projectile.MoveTarget(targetPos);
        }
    }
}