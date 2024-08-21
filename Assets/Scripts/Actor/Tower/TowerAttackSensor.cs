using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackSensor : MonoBehaviour
{
    Tower tower;
    Quaternion originRotation;
    CapsuleCollider capsuleCollider;

    [SerializeField] Transform firePos; // 공격 시작 지점

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
        capsuleCollider.radius = tower.towerDatas.Stats.attackRange;
        towerAttack.Initialize(firePos, tower);
    }
    private void Update()
    {
        RotateToward();

        if (tower.detectActor.targetActor != null)
        {
            towerAttack.StartAttack(tower.detectActor.targetActor);
        }
        else
        {
            towerAttack.StopAttack();
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

            Quaternion currentRot = transform.rotation;
            Quaternion adjustedRot = Quaternion.Euler(currentRot.eulerAngles.x, rot.eulerAngles.y, currentRot.eulerAngles.z);

            transform.rotation = Quaternion.Slerp(adjustedRot, rot, Time.deltaTime * tower.towerDatas.Stats.rotationSpeed);

            float angleDif = Quaternion.Angle(transform.rotation, rot);

            if (angleDif < 0.1f)
            {
                towerAttack.SetReadyToAttack(true, tower.detectActor.targetActor.transform.position);
            }
            else
            {
                towerAttack.SetReadyToAttack(false, Vector3.zero);
            }
        }
    }
}