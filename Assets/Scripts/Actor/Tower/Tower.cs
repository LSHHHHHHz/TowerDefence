using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Actor
{
    public TowerStatusDB towerStatusDB { get; private set; }
    public TowerAttributes towerStatus { get; private set; }
    public TowerAttackSensor towerAttackSensor { get; set; }
    public FSMController<Tower> fsmController { get; private set; }
    public ActorDetector<Monster> detectActor { get; private set; }
    public ActorDetectorRangeImage detectActorRange { get; private set; }
    public TowerData towerData;
    public bool triggerStartAttack;
    protected override void Awake()
    {
        base.Awake();
        towerStatusDB = GameManager.instance.gameEntityData.GetTowerStatusDB(actorId);
        Initialize();
        detectActor = GetComponent<ActorDetector<Monster>>();
        detectActor.detectionRange = towerStatus.attackRange;
        detectActorRange = GetComponentInChildren<ActorDetectorRangeImage>();
        towerAttackSensor = GetComponent<TowerAttackSensor>();
        fsmController = new FSMController<Tower>(this);
        fsmController.ChangeState(new IdleState());
    }
    protected void Update()
    {
        fsmController.FSMUpdate();
    }

    private void OnEnable()
    {
        ActorManager<Tower>.instnace.RegisterActor(this);
    }
    private void OnDisable()
    {
        ActorManager<Tower>.instnace.UnregisterActor(this);
    }
    public void Initialize()
    {
        actoryType = GameManager.instance.gameEntityData.GetActorType(towerStatusDB.type);
        towerStatus = new TowerAttributes(towerStatusDB.hp, towerStatusDB.rotationSpeed, towerStatusDB.combatEffectAmount, towerStatusDB.attackRange, towerStatusDB.attackSpeed);
        ApplyTowerData();
    }
    private void ApplyTowerData()
    {
        // 타워 상태 데이터 적용
        if (towerStatus != null)
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
        towerStatus.currentHP += recovery;
    }
}
