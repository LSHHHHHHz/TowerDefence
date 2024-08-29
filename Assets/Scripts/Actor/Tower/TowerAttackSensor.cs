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

    public TowerBaseAttack towerAttack { get; private set; }
    public bool isReadyToAttack = false;
    public Actor findActor;
    private void Awake()
    {
        tower = GetComponent<Tower>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        originRotation = transform.rotation;
        towerAttack = GetComponent<TowerBaseAttack>();
    }
    private void Start()
    {
        capsuleCollider.radius = tower.towerStatus.attackRange;
        towerAttack.Initialize(firePos, tower.towerStatus.attackSpeed, tower.towerStatus.attackDamage);
    }
    private void Update()
    {
        RotateToward();

        if (tower.detectActor.targetActor != null)
        {
            findActor = tower.detectActor.targetActor;
            towerAttack.StartAttack(tower.detectActor.targetActor);
        }
        else
        {
            findActor = tower.detectActor.targetActor;
            towerAttack.StopAttack();
        }
    }
    private void RotateToward()
    {
        if (tower.detectActor.targetActor == null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originRotation, Time.deltaTime * tower.towerStatus.rotationSpeed);
        }
        else
        {
            Vector3 dir = (tower.detectActor.actorPosition - transform.position).normalized;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir);

            Quaternion currentRot = transform.rotation;
            Quaternion adjustedRot = Quaternion.Euler(currentRot.eulerAngles.x, rot.eulerAngles.y, currentRot.eulerAngles.z);

            transform.rotation = Quaternion.Slerp(adjustedRot, rot, Time.deltaTime * tower.towerStatus.rotationSpeed);

            float angleDif = Quaternion.Angle(transform.rotation, rot);

            if (angleDif < 0.1f)
            {
                isReadyToAttack = true;
                towerAttack.SetReadyToAttack(isReadyToAttack, tower.detectActor.targetActor.transform.position);
            }
            else
            {
                isReadyToAttack = false;
                towerAttack.SetReadyToAttack(isReadyToAttack, Vector3.zero);
            }
        }
    }
}