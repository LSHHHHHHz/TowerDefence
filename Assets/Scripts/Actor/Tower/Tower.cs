using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Actor
{
    public TowerStatus towerStatus { get; private set; }
    protected TowerStatusDB towerStatusDB;
    protected List<Monster> detectedMonsters;
    public TowerAttackSensor towerAttackSensor;
    protected FSMController<Tower> fsmController;
    private TowerData _towerData;
    public TowerData towerData
    {
        get
        {
            if (_towerData == null)
            {
                _towerData = new TowerData();
            }
            return _towerData;
        }
        set
        {
            _towerData = value;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        towerStatusDB = GameManager.instance.gameEntityData.GetTowerStatusDB(actorId);
        Initialize();
        towerAttackSensor = GetComponent<TowerAttackSensor>();
        fsmController = new FSMController<Tower>(this);
        fsmController.ChangeState(new IdleState());
    }
    public void Initialize()
    {
        actoryType = GameManager.instance.gameEntityData.GetActorType(towerStatusDB.type);
        detectActor.Initialized(actoryType);
        towerStatus = new TowerStatus(towerStatusDB.hp, towerStatusDB.rotationSpeed, towerStatusDB.attackDamage, towerStatusDB.attackRange, towerStatusDB.attackSpeed);
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
    public override void DieActor()
    {
        throw new System.NotImplementedException();
    }
}
