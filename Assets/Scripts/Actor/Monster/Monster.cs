using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Monster : Actor
{
    public event Action<Monster> onMonsterDeath;
    public FSMController<Monster> fsmController { get; private set; }
    public MonsterAttributes monsterAttributes { get; private set; }
    public MonsterStatusDB monsterStatusDB { get; private set; }
    public MonsterDebuff monsterDebuff { get; private set; }
    InMonsterCanvas monsterCanvas;
    protected override void Awake()
    {
        base.Awake();
        monsterStatusDB = GameManager.instance.gameEntityData.GetMonsterStatusDB(actorId);
        monsterAttributes = new MonsterAttributes(monsterStatusDB.hp, monsterStatusDB.rotationSpeed, monsterStatusDB.moveSpeed);
        monsterDebuff = new MonsterDebuff();
        fsmController = new FSMController<Monster>(this);
        monsterCanvas = GetComponent<InMonsterCanvas>();
    }
    protected void Update()
    {
        fsmController.FSMUpdate();
    }
    private void OnEnable()
    {
        ActorManager<Monster>.instnace.RegisterActor(this);
        monsterAttributes.currentHP = monsterAttributes.maxHP;
        monsterAttributes.ResetMoveSpeed();
        fsmController.ChangeState(new WalkState());
    }
    private void OnDisable()
    {
        ActorManager<Monster>.instnace.UnregisterActor(this);
        monsterDebuff.ClearDebuffs();
    }
    public override void ReceiveEvent(IEvent ievent)
    {
        if (ievent is SendDamageEvent damageEvent)
        {
            TakeDamage(damageEvent.damage);
        }

        if (ievent is SendSlowDebuffEvent slowDebuffEvent)
        {
            monsterDebuff.AddSlowDebuff(slowDebuffEvent.slowDebuffAmount);
            SetMonsterSpeed(monsterAttributes.originSpeed / monsterDebuff.currentSlowDebuff);
        }
    }
    public override void TakeDamage(int damage)
    {
        monsterAttributes.TakeDamage(damage);
        monsterCanvas.updateHpBar?.Invoke(monsterAttributes.maxHP, monsterAttributes.currentHP);

        if (monsterAttributes.currentHP <= 0)
        {
            DieMonster();
            fsmController.ChangeState(new DieState());
        }
        else
        {
            fsmController.ChangeState(new GetHitState());
        }
    }
    public void TakeOutSlowDebuff(int amount)
    {
        monsterDebuff.RemoveSlowDebuff(amount);
        SetMonsterSpeed(monsterAttributes.originSpeed / monsterDebuff.currentSlowDebuff);
    }

    public void SetMonsterSpeed(float amount)
    {
        monsterAttributes.SetMoveSpeed(Mathf.Max(amount, 0.1f));
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
            player.GetCoin(monsterStatusDB.rewardCoin);
        }
    }
}