using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Actor, IDetect
{
    [SerializeField] string id;
    [SerializeField] ActoryType actoryType;
    protected SetTowerDatas towerDatas;
    protected List<Monster> detectedMonsters;

    private void Update()
    {
        DetectActorsInRange(transform.position, stats.attackRange);
    }
    public void Initialize(string monsterID, ActoryType type)
    {
        towerDatas = GameData.instance.towerData.GetTowerStatusData(monsterID, type);

        if (towerDatas != null)
        {
            profileData = towerDatas.Profile;
            status = new TowerStatus(towerDatas.Status.curentHP, towerDatas.Status.maxHP, towerDatas.Status.currentExp, towerDatas.Status.currentExp);
            stats = new TowerStats(towerDatas.Stats.attackDamage, towerDatas.Stats.attackRange, towerDatas.Stats.moveSpeed, towerDatas.Stats.attackSpeed);

            ApplyTowerData();
        }
        else
        {
            Debug.LogError("데이터 없음 ID: " + monsterID);
        }
    }
    private void ApplyTowerData()
    {
        // 타워 프로필 데이터 적용
        if (profileData != null)
        {
        }

        // 타워 상태 데이터 적용
        if (status != null)
        {
        }
    }
    public void DetectActorsInRange(Vector3 center, float radius)
    {
        Collider[] detectedColliders = Physics.OverlapSphere(center, radius);

        HashSet<Monster> newDetectedActors = new HashSet<Monster>();

        foreach (var collider in detectedColliders)
        {
            Monster actor = collider.GetComponent<Monster>();
            if (actor != null)
            {
                newDetectedActors.Add(actor);
            }
        }

        foreach (var actor in newDetectedActors)
        {
            if (!detectedMonsters.Contains(actor))
            {
                detectedMonsters.Add(actor);
            }
        }

        List<Monster> actorsToRemove = new List<Monster>();
        foreach (var actor in detectedMonsters)
        {
            if (!newDetectedActors.Contains(actor))
            {
                actorsToRemove.Add(actor);
            }
        }
        foreach (var actor in actorsToRemove)
        {
            detectedMonsters.Remove(actor);
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
        status.currentHP += recovery;
    }
    public override void DieActor()
    {
        throw new System.NotImplementedException();
    }
}
