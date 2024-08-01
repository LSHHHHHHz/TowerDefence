using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerAttack : MonoBehaviour
{
    Tower tower;
    Quaternion originRotation;
    bool isReadyToAttack = false;
    Coroutine attackCoroutine;
    float attackDelay;
    float initializedAttackDelay;
    CapsuleCollider capsuleCollider;

    [SerializeField] Transform firePos; //공격 시작 지점
    Vector3 attackDir; //공격 방향
    Vector3 targetPos; //타겟 위치
    [SerializeField] string bulletPrefabPath;
    private void Awake()
    {
        tower = GetComponent<Tower>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        originRotation = transform.rotation;
    }
    private void Start()
    {
        initializedAttackDelay = tower.towerDatas.Stats.attackSpeed;
        attackDelay = 0f;
        capsuleCollider.radius = tower.towerDatas.Stats.attackRange;
    }
    private void Update()
    {
        RotateToward();

        if (isReadyToAttack && tower.detectActor.targetActor != null)
        {
            attackDelay -= Time.deltaTime;
            if (attackDelay <= 0)
            {
                StartAttackAction();
                attackDelay = initializedAttackDelay;
            }
        }
        else
        {
            StopAttack();
        }
    }
    private void RotateToward()
    {
        if (tower.detectActor.targetActor == null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originRotation, Time.deltaTime * tower.towerDatas.Stats.rotationSpeed);
            isReadyToAttack = false;
        }
        else
        {
            Vector3 dir = (tower.detectActor.actorPosition - transform.position).normalized;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir);
            targetPos = tower.detectActor.actorPosition;
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * tower.towerDatas.Stats.rotationSpeed);

            float angleDif = Quaternion.Angle(transform.rotation, rot);
            if (angleDif < 0.1f)
            {
                isReadyToAttack = true;
                if (attackCoroutine == null)
                {
                    StartAttack();
                }
            }
            else
            {
                isReadyToAttack = false;
            }
        }
    }
    private void StartAttack()
    {
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackCorutine());
        }
    }
    private void StopAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }
    IEnumerator AttackCorutine()
    {
        while (true)
        {
            if (tower.detectActor.targetActor != null)
            {
                attackDelay -= Time.deltaTime;
                if (attackDelay <= 0)
                {
                    StartAttackAction();
                    attackDelay = initializedAttackDelay;
                }
            }
            yield return null;
        }
    }
    void StartAttackAction()
    {
        attackDir = (firePos.gameObject.transform.position - tower.gameObject.transform.position).normalized;
        Debug.Log("공격!!!");
        IBullet bullet = PoolManager.instance.GetObjectFromPool(bulletPrefabPath).GetComponent<IBullet>();
        bullet.InitializedBullet(attackDir, targetPos);
        bullet.FireBullet();

        Debug.Log("공격???");
    }
}
