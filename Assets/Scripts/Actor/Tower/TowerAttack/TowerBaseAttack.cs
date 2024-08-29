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
    [SerializeField] float resetTime =2;
    [SerializeField] float animationTime = 0.2f;

    Transform firePos;
    Tower tower;
    private bool isReadyToAttack = false;
    public bool isAttackAction = false;
    private Vector3 targetPos;
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
            attackCoroutine = StartCoroutine(AttackCorutine(targetActor));
        }
    }
    public void StopAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    public void SetReadyToAttack(bool ready, Vector3 targetPos)
    {
        isReadyToAttack = ready;
        if (ready)
        {
            this.targetPos = targetPos;
        }
    }
    IEnumerator AttackCorutine(IActor targetActor)
    {
        while (targetActor != null)
        {
            attackDelay -= Time.deltaTime;
            if (attackDelay <= 0)
            {
                isAttackAction = true;
                StartAttackAction();
                attackDelay = initializedAttackDelay;
            }
            yield return null;
        }      
        StopAttack();
    }
    void StartAttackAction()
    {
        FireBullet(firePos.position, targetPos);
    }
    public void FireBullet(Vector3 firePos, Vector3 targetPos)
    {
        //공격 모션 취하는 시간만큼 시간 설정
        BaseBullet bullet = PoolManager.instance.GetObjectFromPool(bulletPrefabPath).GetComponent<BaseBullet>();
        if (bullet != null)
        {
            bullet.InitializedBullet(firePos, attackDamage);
            bullet.MoveTarget(targetPos);
            StartCoroutine(ResetAttackAction(resetTime));
        }
    }
    IEnumerator ResetAttackAction(float resetTime)
    {
        yield return new WaitForSeconds(resetTime);
        isAttackAction = false;
    }
}