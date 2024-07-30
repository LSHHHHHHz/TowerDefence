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
