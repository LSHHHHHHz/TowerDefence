using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Actor
{   
    protected SetMonsterDatas monsterData;

    public void Initialize(string monsterID, ActoryType type)
    {
        monsterData = GameData.instance.monsterData.GetMonsterStatusData(monsterID, type);

        if (monsterData != null)
        {
            profileData = monsterData.Profile;
            status = new ActorStatus(monsterData.Status.maxHP, 0);
            stats = new ActorStats(monsterData.Stats.attackDamage,monsterData.Stats.attackRange,stats.moveSpeed,stats.attackSpeed);

            ApplyMonsterData();
        }
        else
        {
            Debug.LogError("������ ���� ID: " + monsterID);
        }
    }

    private void ApplyMonsterData()
    {
        // ���� ������ ������ ����
        if (profileData != null)
        {
        }

        // ���� ���� ������ ����
        if (status != null)
        {
        }

        // ���� ���� ������ ����
        if (monsterData != null)
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
    public override void DieActor()
    {
        throw new System.NotImplementedException();
    }
}
