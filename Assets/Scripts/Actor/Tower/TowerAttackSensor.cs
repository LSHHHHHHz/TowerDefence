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
    bool isAttack = false;

    public ActorDetector<Monster> detectActor;
    private void Awake()
    {
        tower = GetComponent<Tower>();

        capsuleCollider = GetComponent<CapsuleCollider>();
        originRotation = transform.rotation;
        towerBaseAttack = GetComponent<TowerBaseAttack>();
    }
    private void Start()
    {
        detectActor = tower.detectActor;
        capsuleCollider.radius = tower.towerStatus.attackRange;
        towerBaseAttack.Initialize(firePos, tower.towerStatus.attackSpeed, tower.towerStatus.attackStatusAmount);
        towerBaseAttack.isAttackActionFalse += CheckAttackFalse;
        towerBaseAttack.isAttackActionTrue += CheckAttackTrue;
    }
    private void Update()
    {
        RotateToward();

        if(detectActor.targetActor != null )
        {
            //직접 사용
        }
        if (tower.detectActor.targetActor != null && tower.triggerStartAttack && !isAttack)
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
            isReadyToAttack = false;
            transform.rotation = Quaternion.Slerp(transform.rotation, originRotation, Time.deltaTime * tower.towerStatus.rotationSpeed);
        }
        else
        {
            Vector3 dir = (tower.detectActor.targetActor.transform.position - transform.position).normalized;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * tower.towerStatus.rotationSpeed * 20);

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
    void CheckAttackTrue()
    {
        isAttack = true;
    }
    void CheckAttackFalse()
    {
        isAttack = false;
    }
}