using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Actor
{
    [SerializeField] string id;//이건 나중에 수정 (Test)
    public SetTowerDatas towerDatas { get; private set; }
    protected List<Monster> detectedMonsters;
    protected override void Awake()
    {
        Initialize(id, actoryType); //이건 나중에 수정 (Test)
        base.Awake();
    }
    public void Initialize(string TowerID, ActorType type)
    {
        towerDatas = GameData.instance.towerData.GetTowerStatusData(TowerID, type);

        if (towerDatas != null)
        {
            profileData = towerDatas.Profile;
            actoryType = towerDatas.Profile.type;
            status = new TowerStatus(towerDatas.Status.curentHP, towerDatas.Status.maxHP, towerDatas.Status.currentExp, towerDatas.Status.currentExp);
            stats = new TowerStats(towerDatas.Stats.attackDamage, towerDatas.Stats.attackRange, towerDatas.Stats.moveSpeed, towerDatas.Stats.attackSpeed);

            ApplyTowerData();
        }
        else
        {
            Debug.LogError("타워 데이터 없음 ID: " + TowerID);
        }
    }
    private void ApplyTowerData()
    {
        // 타워 프로필 데이터 적용
        if (profileData != null)
        {
        }

        // 타워 상태 데이터 적용
        if (status != null)
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
        status.currentHP += recovery;
    }
    public override void DieActor()
    {
        throw new System.NotImplementedException();
    }
}
