using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Monster : Actor
{
    public event Action onMonsterDeath;
    public FSMController<Monster> fsmController { get; private set; }
    public MonsterStatus monsterStatus { get; private set; }
    public MonsterStatusDB monsterStatusDB { get; private set; }
    public event Action<int, int> onDamagedAction;

    List<int> monsterSlowDebuffList = new List<int>();
    int currentSlowDebuff = 1;
    protected override void Awake()
    {
        base.Awake();
        monsterStatusDB = GameManager.instance.gameEntityData.GetMonsterStatusDB(actorId);
        Initialize();
        fsmController = new FSMController<Monster>(this);
        fsmController.ChangeState(new WalkState());
    }
    protected void Update()
    {
        fsmController.FSMUpdate();
    }
    private void OnEnable()
    {
        onMonsterDeath += GameManager.instance.stageManager.DeathMonster;
    }
    private void OnDisable()
    {
        monsterSlowDebuffList.Clear(); //디버프가 있는 상태에서 제거되면 계속 남아있어서 Clear함
        currentSlowDebuff = 1;
        DieMonser();
        Debug.LogError("와오");
        onMonsterDeath -= GameManager.instance.stageManager.DeathMonster;
    }
    public void Initialize()
    {
        actoryType = GameManager.instance.gameEntityData.GetActorType(monsterStatusDB.type);
        detectActor.Initialized(actoryType);
        monsterStatus = new MonsterStatus(monsterStatusDB.hp, monsterStatusDB.rotationSpeed, monsterStatusDB.moveSpeed);
        ApplyMonsterData();
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
        if (ievent is SendSlowDebuffEvent slowDebuffEvent)
        {
            TakeSlowDebuff(slowDebuffEvent.slowDebuffAmount);
        }
    }
    public override void TakeDamage(int damage)
    {
        monsterStatus.TakeDamage(damage);
        onDamagedAction?.Invoke(monsterStatus.maxHP, damage);
        fsmController.ChangeState(new GetHitState());
    }
    public void TakeSlowDebuff(int amount)
    {
        monsterSlowDebuffList.Add(amount);

        if (monsterSlowDebuffList.Count > 0)
        {
            currentSlowDebuff = monsterSlowDebuffList[0];
            for (int i = 1; i < monsterSlowDebuffList.Count; i++)
            {
                if (monsterSlowDebuffList[i] > currentSlowDebuff)
                {
                    currentSlowDebuff = monsterSlowDebuffList[i];
                }
            }
        }
        else
        {
            currentSlowDebuff = 1;
        }
        monsterStatus.SetMoveSpeed(GameManager.instance.gameEntityData.GetMonsterStatusDB(actorId).moveSpeed / currentSlowDebuff);
    }
    public void TakeOutSlowDebuff(int amount)
    {
        if (monsterSlowDebuffList.Contains(amount))
        {
            monsterSlowDebuffList.Remove(amount);

            if (monsterSlowDebuffList.Count > 0)
            {
                currentSlowDebuff = monsterSlowDebuffList[0];

                for (int i = 1; i < monsterSlowDebuffList.Count; i++)
                {
                    if (monsterSlowDebuffList[i] > currentSlowDebuff)
                    {
                        currentSlowDebuff = monsterSlowDebuffList[i];
                    }
                }
            }
            else
            {
                currentSlowDebuff = 1;
            }
            monsterStatus.SetMoveSpeed(GameManager.instance.gameEntityData.GetMonsterStatusDB(actorId).moveSpeed / currentSlowDebuff);
        }
    }
    private void DieMonser()
    {
        onMonsterDeath?.Invoke();
        GiveCoinToPlayer();
    }
    private void GiveCoinToPlayer()
    {
        if (player != null)
        {
            player.GetCoin(monsterStatusDB.coin);
        }
    }
}
