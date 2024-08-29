using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBaseAttack : MonoBehaviour
{
    Coroutine attackCoroutine;
    float attackDelay;
    float initializedAttackDelay;
    int attackDamage;
    [SerializeField] string bulletPrefabPath;
    [SerializeField] float resetTime = 0.2f; // 공격 후 초기화 시간
    public bool isAttackAction;
    Transform firePos;
    private bool isReadyToAttack = false;
    private Vector3 targetPos;

    private void Update()
    {
        Debug.LogError(isAttackAction);
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
        isAttackAction = false; 
    }
    public void SetReadyToAttack(bool ready, Vector3 targetPos)
    {
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
        FireBullet(firePos.position, targetPos);
    }
    public void FireBullet(Vector3 firePos, Vector3 targetPos)
    {
        BaseBullet bullet = PoolManager.instance.GetObjectFromPool(bulletPrefabPath).GetComponent<BaseBullet>();
        if (bullet != null)
        {
            bullet.InitializedBullet(firePos, attackDamage);
            bullet.MoveTarget(targetPos);
        }
    }
}