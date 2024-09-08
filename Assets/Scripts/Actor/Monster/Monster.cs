using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Monster : Actor
{
    public event Action<Monster> onMonsterDeath;
    public FSMController<Monster> fsmController { get; private set; }
    public MonsterStatus monsterStatus { get; private set; }
    public MonsterStatusDB monsterStatusDB { get; private set; }
    List<int> monsterSlowDebuffList = new List<int>();
    int currentSlowDebuff = 1;
    InMonsterCanvas monsterCanvas;
    protected override void Awake()
    {
        base.Awake();
        monsterStatusDB = GameManager.instance.gameEntityData.GetMonsterStatusDB(actorId);
        Initialize();
        fsmController = new FSMController<Monster>(this);
        monsterCanvas = GetComponent<InMonsterCanvas>();        
    }
    protected void Update()
    {
        fsmController.FSMUpdate();
    }
    private void OnEnable()
    {
        fsmController.ChangeState(new WalkState());
    }
    private void OnDisable()
    {
        monsterSlowDebuffList.Clear(); //������� �ִ� ���¿��� ���ŵǸ� ��� �����־ Clear��
        currentSlowDebuff = 1;
        Debug.LogError("�Ϳ�");
    }
    public void Initialize()
    {
        actoryType = GameManager.instance.gameEntityData.GetActorType(monsterStatusDB.type);
       //���� detectActor.Initialized(actoryType);
        monsterStatus = new MonsterStatus(monsterStatusDB.hp, monsterStatusDB.rotationSpeed, monsterStatusDB.moveSpeed);
        ApplyMonsterData();
        monsterStatus.currentHP = 40;
    }
    private void ApplyMonsterData()
    {
        // ���� ���� ������ ����
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
        monsterCanvas.updateHpBar?.Invoke(monsterStatus.maxHP, monsterStatus.currentHP);
        if (monsterStatus.currentHP <= 0)
        {
            DieMonster();
            fsmController.ChangeState(new DieState());
        }
        else
        {
            fsmController.ChangeState(new GetHitState());
        }
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
            SetMonsterSpeed(GameManager.instance.gameEntityData.GetMonsterStatusDB(actorId).moveSpeed / currentSlowDebuff);
        }
    }
    public void SetMonsterSpeed(float amount)
    {
        monsterStatus.SetMoveSpeed(amount);
    }
    private void DieMonster()
    {
        EventManager.instance.KilledMonster();
        monsterCanvas.OnEnableCoin();
        onMonsterDeath?.Invoke(this);
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
