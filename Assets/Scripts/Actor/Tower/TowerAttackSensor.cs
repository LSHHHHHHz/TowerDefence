using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerAttackSensor : MonoBehaviour
{
    Tower tower;
    Quaternion originRotation;
    bool isReadyToAttack = true;
    Coroutine attackCoroutine;
    float attackDelay;
    float initializedAttackDelay;
    CapsuleCollider capsuleCollider;

    [SerializeField] Transform firePos; //공격 시작 지점
    Vector3 attackDir; //공격 방향
    Vector3 targetPos; //타겟 위치
    [SerializeField] string bulletPrefabPath;

    TowerBaseAttack towerAttack;
    private void Awake()
    {
        tower = GetComponent<Tower>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        originRotation = transform.rotation;
        towerAttack = GetComponent<TowerBaseAttack>();
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
            StartAttack();
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

            Quaternion currentRot = transform.rotation;
            Quaternion adjustedRot = Quaternion.Euler(currentRot.eulerAngles.x, rot.eulerAngles.y, currentRot.eulerAngles.z);

            transform.rotation = Quaternion.Slerp(adjustedRot, rot, Time.deltaTime * tower.towerDatas.Stats.rotationSpeed);

            float angleDif = Quaternion.Angle(transform.rotation, rot);

            if (angleDif < 0.1f)
            {
                isReadyToAttack = true;
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
        Debug.Log("들어오나");
        attackDir = (firePos.gameObject.transform.position - tower.gameObject.transform.position).normalized;
        towerAttack.FireBullet(attackDir, targetPos);
    }
}
