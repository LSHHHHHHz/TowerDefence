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

    public TowerBaseAttack towerBaseAttack;
    public bool isReadyToAttack = false;
    public Actor findActor;
    private void Awake()
    {
        tower = GetComponent<Tower>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        originRotation = transform.rotation;
        towerBaseAttack = GetComponent<TowerBaseAttack>();
    }
    private void Start()
    {
        capsuleCollider.radius = tower.towerStatus.attackRange;
        towerBaseAttack.Initialize(firePos, tower.towerStatus.attackSpeed, tower.towerStatus.attackStatusAmount); 
    }
    private void Update()
    {
        RotateToward();

        if (tower.detectActor.targetActor != null)
        {
            findActor = tower.detectActor.targetActor;
            towerBaseAttack.StartAttack(tower.detectActor.targetActor);
        }
        else
        {
            findActor = tower.detectActor.targetActor;
            towerBaseAttack.StopAttack();
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
            Vector3 dir = (tower.detectActor.targetPosition - transform.position).normalized;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir);

            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * tower.towerStatus.rotationSpeed * 2);

            float angleDif = Quaternion.Angle(transform.rotation, rot);

            if (angleDif < 0.3f)
            {
                isReadyToAttack = true;
                towerBaseAttack.SetReadyToAttack(isReadyToAttack, tower.detectActor.targetActor.transform.position);
            }
            else
            {
                isReadyToAttack = false;
                towerBaseAttack.SetReadyToAttack(isReadyToAttack, Vector3.zero);
            }
        }
    }
}