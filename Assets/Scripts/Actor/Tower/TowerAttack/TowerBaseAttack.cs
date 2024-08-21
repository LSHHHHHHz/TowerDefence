using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBaseAttack : MonoBehaviour
{
    Coroutine attackCoroutine;
    float attackDelay;
    float initializedAttackDelay;
    [SerializeField] string bulletPrefabPath;

    Transform firePos;
    Tower tower;

    private bool isReadyToAttack = false;
    private Vector3 targetPos;

    public void Initialize(Transform firePos, Tower tower)
    {
        this.firePos = firePos;
        this.tower = tower;
        initializedAttackDelay = tower.towerDatas.Stats.attackSpeed;
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
        BaseBullet bullet = PoolManager.instance.GetObjectFromPool(bulletPrefabPath).GetComponent<BaseBullet>();
        if (bullet != null)
        {
            bullet.InitializedBullet(firePos, tower.towerDatas.Stats.attackDamage);
            bullet.MoveTarget(targetPos);
        }
    }
}