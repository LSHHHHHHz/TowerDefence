using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Actor
{
    public TowerStatus towerStatus { get; private set; }
    protected List<Monster> detectedMonsters;
    public TowerAttackSensor towerAttackSensor;
    protected override void Awake()
    {
        base.Awake();
        Initialize(actorId);
        towerAttackSensor = GetComponent<TowerAttackSensor>();
    }
    public void Initialize(string TowerID)
    {
        if (TowerID != "" && TowerID != null)
        {
            actoryType = GameManager.instance.gameEntityData.GetActorType(actorStatusDB.type);
            towerStatus = new TowerStatus(actorStatusDB.hp, actorStatusDB.rotationSpeed, actorStatusDB.attackDamage, actorStatusDB.attackRange, actorStatusDB.attackSpeed);
            ApplyTowerData();
        }
        else
        {
            Debug.LogError("타워 데이터 없음 ID: " + TowerID);
        }
    }
    private void ApplyTowerData()
    {
        // 타워 상태 데이터 적용
        if (towerStatus != null)
        {
        }
    }
    public override void ReceiveEvent(IEvent ievent)
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
    public void RecoveryHP(int recovery)
    {
        towerStatus.currentHP += recovery;
    }
    public override void DieActor()
    {
        throw new System.NotImplementedException();
    }
}
