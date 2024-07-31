using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerAttack : MonoBehaviour
{
    Tower tower;
    Quaternion originRotation;
    bool isOriginRotation = false;
    bool isReadyToAttack = false;
    bool findMonster;
    Coroutine attackCoroutine;
    float attackDelay;
    //코루틴을 멈춰도 attckDelay는 계속 줄여야함
    private void Awake()
    {
        tower = GetComponent<Tower>();
        originRotation = transform.rotation;
    }
    private void Start()
    {
        attackDelay = tower.towerDatas.Stats.attackSpeed;
    }
    private void Update()
    {
        RotateToward();
        if (isReadyToAttack) 
        {
            StartAttack(3);
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
        }
        else
        {
            Vector3 dir = (tower.detectActor.actorPosition - transform.position).normalized;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * tower.towerDatas.Stats.rotationSpeed);

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
    private void StartAttack(float attackSpeed)
    {
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(StartBaseAttackCoroutine(2f));
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
    IEnumerator StartBaseAttackCoroutine(float attackSpeed)
    {
        while (true)
        {
            StartAttackAction();
            yield return new WaitForSeconds(attackSpeed);
        }
    }
    void StartAttackAction()
    {
        Debug.Log("공격!!!");
    }
}
