using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackSensor : MonoBehaviour
{
    Tower tower;
    Quaternion originRotation;
    [SerializeField] Transform firePos; // 공격 시작 지점

    public TowerBaseAttack towerBaseAttack;
    public bool isReadyToAttack = false;
    public IActor findActor;
    public bool isAttack = false;
    public bool isActiveAttack;
    public ActorDetector<Monster> detectActor;
    private void Awake()
    {
        tower = GetComponent<Tower>();
        originRotation = transform.rotation;
        towerBaseAttack = GetComponent<TowerBaseAttack>();

    }
    private void Start()
    {
        detectActor = tower.detectActor;
        towerBaseAttack.Initialize(firePos, tower.towerStatus.attackSpeed, tower.towerStatus.attackStatusAmount);
        towerBaseAttack.isAttackActionFalse += CheckAttackFalse;
        towerBaseAttack.isAttackActionTrue += CheckAttackTrue;
    }
    private void OnEnable()
    {
        EventManager.instance.onSetTower += SetTowerOnGround;
    }
    private void OnDisable()
    {
        EventManager.instance.onSetTower -= SetTowerOnGround;
    }
    private void Update()
    {
        if (isActiveAttack)
        {
            RotateToward();
            if (tower.detectActor.targetActor != null && isReadyToAttack && !isAttack)
            {
                findActor = tower.detectActor.targetActor;
                towerBaseAttack.StartAttack(tower.detectActor.targetActor);
                isAttack = true;
                Debug.Log("어디1");
            }
            else if(tower.detectActor.targetActor == null )
            {
                findActor = tower.detectActor.targetActor;
               // towerBaseAttack.StopAttack();
                Debug.LogError("어디2");
            }
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
            if (angleDif < 0.5f)
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
    void SetTowerOnGround()
    {
        isActiveAttack = true;
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