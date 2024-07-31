using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Actor
{
    [SerializeField] string id;//�̰� ���߿� ���� (Test)
    public SetTowerDatas towerDatas { get; private set; }
    protected List<Monster> detectedMonsters;
    protected override void Awake()
    {
        Initialize(id, actoryType); //�̰� ���߿� ���� (Test)
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
            Debug.LogError("Ÿ�� ������ ���� ID: " + TowerID);
        }
    }
    private void ApplyTowerData()
    {
        // Ÿ�� ������ ������ ����
        if (profileData != null)
        {
        }

        // Ÿ�� ���� ������ ����
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
