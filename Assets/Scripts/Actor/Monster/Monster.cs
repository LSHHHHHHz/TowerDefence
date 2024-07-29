using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Actor
{
    protected ActorProfileData profileData;
    protected SetMonsterDatas monsterData;

    public void Initialize(string monsterID, ActoryType type)
    {
        monsterData = GameData.instance.monsterData.GetMonsterStatusData(monsterID, type);

        if (monsterData != null)
        {
            profileData = monsterData.Profile;
            status = new ActorStatus(monsterData.Status.maxHP, 0);
            stats = new ActorStats(monsterData.Status.damage,0,5);

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
    public override void DieActor()
    {
        throw new System.NotImplementedException();
    }
}
