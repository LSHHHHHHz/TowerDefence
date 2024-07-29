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
            Debug.LogError("데이터 없음 ID: " + monsterID);
        }
    }

    private void ApplyMonsterData()
    {
        // 몬스터 프로필 데이터 적용
        if (profileData != null)
        {
        }

        // 몬스터 상태 데이터 적용
        if (status != null)
        {
        }

        // 몬스터 보상 데이터 적용
        if (monsterData != null)
        {
        }
    }
    public override void DieActor()
    {
        throw new System.NotImplementedException();
    }
}
