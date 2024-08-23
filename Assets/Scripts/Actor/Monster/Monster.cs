using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Monster : Actor
{
    protected MonsterStatus monsterStatus;
    protected override void Awake()
    {
        base.Awake();
        Initialize(actorId);
    }
    public void Initialize(string monsterID)
    {
        if (monsterID != "" && monsterID != null)
        {
            actoryType = GameManager.instance.gameEntityData.GetActorType(actorStatusDB.type);
            monsterStatus = new MonsterStatus(actorStatusDB.hp, actorStatusDB.rotationSpeed, actorStatusDB.moveSpeed);
            ApplyMonsterData();
        }
        else
        {
            Debug.LogError("데이터 없음 ID: " + monsterID);
        }
    }
    private void ApplyMonsterData()
    {
        // 몬스터 상태 데이터 적용
        if (monsterStatus != null)
        {
        }
    }
    public override void ReceiveEvent(IEvent ievent)
    {
        if (ievent is SendDamageEvent damageEvent)
        {
            TakeDamage(damageEvent.damage);
        }
    }
    public override void TakeDamage(int damage)
    {
        monsterStatus.TakeDamage(damage);
    }
    public override void DieActor()
    {
        throw new System.NotImplementedException();
    }
}
