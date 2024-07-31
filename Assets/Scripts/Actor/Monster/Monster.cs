using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Actor
{
    [SerializeField] string id;//이건 나중에 수정
    protected SetMonsterDatas monsterData;
    protected override void Awake()
    {
        Initialize(id, actoryType); //이건 나중에 수정
        base.Awake();
    }
    public void Initialize(string monsterID, ActorType type)
    {
        monsterData = GameData.instance.monsterData.GetMonsterStatusData(monsterID, type);

        if (monsterData != null)
        {
            profileData = monsterData.Profile;
            actoryType = monsterData.Profile.type;
            status = new MonsterStatus(monsterData.Status.maxHP, 0);
            stats = new MonsterStats(monsterData.Stats.attackDamage, monsterData.Stats.attackRange, stats.moveSpeed, stats.attackSpeed);

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
