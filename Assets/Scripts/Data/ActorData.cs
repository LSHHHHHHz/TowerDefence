using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum ActorType
{
    NormarMonster,
    BossMonster,
    NormarTower,
    ChampionTower
}
public class ActorProfileData
{
    public string actorName;
    public string iconPath;
    public string prefabPath;
    public ActorType type;
}
public class ActorStatusData
{
    public int maxHP;
    public int curentHP;
}
public class ActorStatsData
{
    public int attackDamage;
    public int attackRange;
    public int attackSpeed;
    public int moveSpeed;
    public float rotationSpeed;
}
public class MonsterRewardData
{
    public int rewardCoin;
    public int rewardExp;
}
public class TowerProfileData : ActorProfileData
{
    public int buyPrice;
    public int sellPrice;
    public int upgradeTime;
}
public class TowerStatusData : ActorStatusData
{
    public int maxExp;
    public int currentExp;
}
public class TowerStatsData : ActorStatsData
{

}

[Serializable]
public class MonsterData
{
    public List<MonsterEntryDatas> normarMonsterEntryDatas;
    public List<MonsterEntryDatas> bossMonsterEntryDatas;
    public MonsterData()
    {
        normarMonsterEntryDatas = new List<MonsterEntryDatas>();
        bossMonsterEntryDatas = new List<MonsterEntryDatas>();
        InitializeMonsterData();
    }

    public void AddNormarMonster(string monsterID, NormarMonsterProfileData monsterProfileData, NormarMonsterStatusData monsterStatusData, NormarMonsterStatsData normarMonsterStatsData, NormarMonsterRewardData monsterRewardData)
    {
        SetMonsterDatas data = new SetMonsterDatas(monsterProfileData, monsterStatusData,normarMonsterStatsData, monsterRewardData);
        normarMonsterEntryDatas.Add(new MonsterEntryDatas(monsterID, data));
    }
    public void AddBossMonster(string monsterID, BossMonsterProfileData bossMonsterProfileData, BossMonsterStatusData bossMonsterStatusData, BossMonsterStatsData bossMonsterStatsData, BossMonsterRewardData bossMonsterRewardData)
    {
        SetMonsterDatas data = new SetMonsterDatas(bossMonsterProfileData, bossMonsterStatusData, bossMonsterStatsData, bossMonsterRewardData);
        bossMonsterEntryDatas.Add(new MonsterEntryDatas(monsterID, data));
    }
    public void InitializeMonsterData()
    {
        AddNormarMonster("노말몬스터1",
            new NormarMonsterProfileData("골렘1", "none", "Prefabs/Monster/Normar/LV1_Golem", ActorType.NormarMonster),
            new NormarMonsterStatusData(500, 500),
            new NormarMonsterStatsData(0,0,0,5,10),
            new NormarMonsterRewardData(10, 10));
        AddNormarMonster("노말몬스터2",
            new NormarMonsterProfileData("골레 : 노말몬스터2", "none", "none", ActorType.NormarMonster),
            new NormarMonsterStatusData(1000, 1000),
             new NormarMonsterStatsData(0, 0, 0, 5, 10),
            new NormarMonsterRewardData(20, 20));
        AddNormarMonster("노말몬스터3",
            new NormarMonsterProfileData("이름 : 노말몬스터3", "none", "none", ActorType.NormarMonster),
            new NormarMonsterStatusData(1500, 1500),
             new NormarMonsterStatsData(0, 0, 0, 5, 10),
            new NormarMonsterRewardData(20, 20));
        AddBossMonster("보스몬스터1",
           new BossMonsterProfileData("이름 : 보스몬스터1", "none", "none", ActorType.BossMonster),
           new BossMonsterStatusData(5000, 5000),
            new BossMonsterStatsData(50, 5, 5, 4, 10),
           new BossMonsterRewardData(100, 100));
        AddBossMonster("보스몬스터2",
           new BossMonsterProfileData("이름 : 보스몬스터2", "none", "none", ActorType.BossMonster),
           new BossMonsterStatusData(8000, 8000),
            new BossMonsterStatsData(100, 5, 5, 4, 10),
           new BossMonsterRewardData(200, 200));
        AddBossMonster("보스몬스터3",
           new BossMonsterProfileData("이름 : 보스몬스터3", "none", "none", ActorType.BossMonster),
           new BossMonsterStatusData(10000, 10000),
            new BossMonsterStatsData(200, 5, 5, 4, 10),
           new BossMonsterRewardData(300, 300));
    }
    public SetMonsterDatas GetMonsterStatusData(string monsterNumber, ActorType type)
    {
        List<MonsterEntryDatas> data = null;
        string monsterType = "";
        switch (type)
        {
            case ActorType.NormarMonster:
                data = normarMonsterEntryDatas;
                monsterType = "노말몬스터";
                break;
            case ActorType.BossMonster:
                data = bossMonsterEntryDatas;
                monsterType = "보스몬스터";
                break;
        }
        foreach (var entry in data)
        {
            if (entry.GetMonsterID() == monsterType+monsterNumber)
            {
                return entry.GetMonsterData();
            }
        }
        return null;
    }
}
[Serializable]
public class MonsterEntryDatas
{
    public string actorID;
    public SetMonsterDatas monsterDatas;
    public MonsterEntryDatas(string id, SetMonsterDatas datas)
    {
        this.actorID = id;
        this.monsterDatas = datas;
    }
    public string GetMonsterID()
    {
        return actorID;
    }

    public SetMonsterDatas GetMonsterData()
    {
        return monsterDatas;
    }
}
[Serializable]
public class SetMonsterDatas
{
    public ActorProfileData Profile;
    public ActorStatusData Status;
    public ActorStatsData Stats;
    public MonsterRewardData Reward;

    public SetMonsterDatas(ActorProfileData profile, ActorStatusData status, ActorStatsData stats, MonsterRewardData reward)
    {
        Profile = profile;
        Status = status;
        Stats = stats;
        Reward = reward;
    }
}
[Serializable]
public class NormarMonsterProfileData : ActorProfileData
{
    public NormarMonsterProfileData(string monsterName, string iconPath, string prefabPath, ActorType type)
    {
        this.actorName = monsterName;
        this.iconPath = iconPath;
        this.prefabPath = prefabPath;
        this.type = type;
    }
}
[Serializable]
public class NormarMonsterStatusData : ActorStatusData
{
    public NormarMonsterStatusData(int maxHP, int currentHP)
    {
        this.maxHP = maxHP;
        this.curentHP = currentHP;
    }
}
[Serializable]
public class NormarMonsterStatsData : ActorStatsData
{
    public NormarMonsterStatsData(int damage, int range, int attackSpeed, int moveSpeed, float rotationSpeed)
    {
        this.attackDamage = damage;
        this.attackRange = range;
        this.attackSpeed = attackSpeed;
        this.moveSpeed = moveSpeed;
        this.rotationSpeed = rotationSpeed;
    }
}
[Serializable]
public class NormarMonsterRewardData : MonsterRewardData
{
    public NormarMonsterRewardData(int rewardCoin, int rewardExp)
    {
        this.rewardCoin = rewardCoin;
        this.rewardExp = rewardExp;
    }
}
[Serializable]
public class BossMonsterProfileData : ActorProfileData
{
    public BossMonsterProfileData(string monsterName, string iconPath, string prefabPath, ActorType type)
    {
        this.actorName = monsterName;
        this.iconPath = iconPath;
        this.prefabPath = prefabPath;
        this.type = type;
    }
}
[Serializable]
public class BossMonsterStatusData : ActorStatusData
{
    public BossMonsterStatusData(int maxHP, int currentHP)
    {
        this.maxHP = maxHP;
        this.curentHP = currentHP;
    }
}
[Serializable]
public class BossMonsterStatsData : ActorStatsData
{
    public BossMonsterStatsData(int damage, int range, int attackSpeed, int moveSpeed, float rotationSpeed)
    {
        this.attackDamage = damage;
        this.attackRange = range;
        this.attackSpeed = attackSpeed;
        this.moveSpeed = moveSpeed;
        this.rotationSpeed = rotationSpeed;
    }
}
[Serializable]
public class BossMonsterRewardData : MonsterRewardData
{
    public BossMonsterRewardData(int rewardCoin, int rewardExp)
    {
        this.rewardCoin = rewardCoin;
        this.rewardExp = rewardExp;
    }
}
/// <summary>
/// 
/// </summary>
[Serializable]
public class TowerData
{
    public List<TowerEntryDatas> normarTowerEntryDatas;
    public List<TowerEntryDatas> chapionTowerEntryDatas;
    public TowerData()
    {
        normarTowerEntryDatas = new List<TowerEntryDatas>();
        chapionTowerEntryDatas = new List<TowerEntryDatas>();
        InitializeMonsterData();
    }

    public void AddNormarTower(string monsterID, NormarTowerProfileData normarTowerProfileData, NormarTowerStatusData normarTowerStatusData, NormarTowerStatsData normarTowerStatsData)
    {
        SetTowerDatas data = new SetTowerDatas(normarTowerProfileData, normarTowerStatusData, normarTowerStatsData);
        normarTowerEntryDatas.Add(new TowerEntryDatas(monsterID, data));
    }
    public void AddChampionTower(string monsterID, ChamPionProfileData chamPionProfileData, ChamPionStatusData chamPionStatusData, ChamPionStatsData chamPionStatsData)
    {
        SetTowerDatas data = new SetTowerDatas(chamPionProfileData, chamPionStatusData, chamPionStatsData);
        chapionTowerEntryDatas.Add(new TowerEntryDatas(monsterID, data));
    }
    public void InitializeMonsterData()
    {
        AddNormarTower("static11",
            new NormarTowerProfileData("방어타워", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.NormarTower, 5000, 3000, 30),
            new NormarTowerStatusData(500, 500, 1000, 0),
            new NormarTowerStatsData(10, 10, 5, 0, 10));
        AddNormarTower("static12",
            new NormarTowerProfileData("바어타워", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.NormarTower, 5000, 3000, 60),
            new NormarTowerStatusData(1000, 1000, 2000, 0),
            new NormarTowerStatsData(20, 10, 5, 0, 10));
        AddNormarTower("static13",
            new NormarTowerProfileData("방어타워", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.NormarTower, 5000, 3000, 90),
            new NormarTowerStatusData(1500, 1500, 3000, 0),
            new NormarTowerStatsData(30, 10, 5, 0, 10));
        AddNormarTower("nor11",
            new NormarTowerProfileData("불타워", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.NormarTower, 5000, 3000, 30),
            new NormarTowerStatusData(500, 500, 1000,0),
            new NormarTowerStatsData(10, 10, 5, 0, 10));
        AddNormarTower("nor12",
            new NormarTowerProfileData("불타워", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.NormarTower, 5000, 3000, 60),
            new NormarTowerStatusData(1000, 1000,2000,0),
            new NormarTowerStatsData(20, 10, 5, 0, 10));
        AddNormarTower("nor13",
            new NormarTowerProfileData("불타워", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.NormarTower, 5000, 3000, 90),
            new NormarTowerStatusData(1500, 1500,3000,0),
            new NormarTowerStatsData(30, 10, 5, 0, 10));
        AddNormarTower("nor21",
            new NormarTowerProfileData("번개타워", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.NormarTower, 5000, 3000, 30),
            new NormarTowerStatusData(500, 500,1000,0),
            new NormarTowerStatsData(10, 10, 5, 0, 10));
        AddNormarTower("nor22",
            new NormarTowerProfileData("번개타워", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.NormarTower, 5000, 3000, 60),
            new NormarTowerStatusData(1000, 1000, 2000, 0),
            new NormarTowerStatsData(20, 10, 5, 0, 10));
        AddNormarTower("nor23",
            new NormarTowerProfileData("번개타워", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.NormarTower, 5000, 3000, 90),
            new NormarTowerStatusData(1500, 1500, 3000, 0),
            new NormarTowerStatsData(30, 10, 5, 0, 10));
        AddNormarTower("nor31",
            new NormarTowerProfileData("물타워", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.NormarTower, 5000, 3000, 30),
            new NormarTowerStatusData(500, 500, 1000, 0),
            new NormarTowerStatsData(10, 10, 5, 0, 10));
        AddNormarTower("nor32",
            new NormarTowerProfileData("물타워", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.NormarTower, 5000, 3000, 60),
            new NormarTowerStatusData(1000, 1000, 2000, 0),
            new NormarTowerStatsData(20, 10, 5, 0, 10));
        AddNormarTower("nor33",
            new NormarTowerProfileData("물타워", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.NormarTower, 5000, 3000, 90),
            new NormarTowerStatusData(1500, 1500, 3000, 0),
            new NormarTowerStatsData(30, 10, 5, 0, 10));

        AddChampionTower("cham11",
            new ChamPionProfileData("슬로우챔편", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.ChampionTower, 10000, 5000, 30),
            new ChamPionStatusData(1000, 1000, 1000, 0),
            new ChamPionStatsData(20, 15, 3, 0, 10));
        AddChampionTower("cham12",
            new ChamPionProfileData("슬로우챔편", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.ChampionTower, 10000, 5000, 30),
            new ChamPionStatusData(2000, 2000, 2000, 0),
            new ChamPionStatsData(50, 15, 3, 0, 10));
        AddChampionTower("cham13",
            new ChamPionProfileData("슬로우챔편", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.ChampionTower, 10000, 5000, 30),
            new ChamPionStatusData(5000, 5000, 3000, 0),
            new ChamPionStatsData(100, 15, 3, 0, 10));
        AddChampionTower("cham21",
            new ChamPionProfileData("공속버프챔편", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.ChampionTower, 10000, 5000, 30),
            new ChamPionStatusData(1000, 1000, 1000, 0),
            new ChamPionStatsData(20, 15, 3, 0, 10));
        AddChampionTower("cham22",
            new ChamPionProfileData("공속버프챔편", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.ChampionTower, 10000, 5000, 30),
            new ChamPionStatusData(2000, 2000, 2000, 0),
            new ChamPionStatsData(50, 15, 3, 0, 10));
        AddChampionTower("cham23",
            new ChamPionProfileData("공속버프챔편", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.ChampionTower, 10000, 5000, 30),
            new ChamPionStatusData(5000, 5000, 3000, 0),
            new ChamPionStatsData(100, 15, 3, 0, 10));
        AddChampionTower("cham31",
            new ChamPionProfileData("공격버프챔편", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.ChampionTower, 10000, 5000, 30),
            new ChamPionStatusData(1000, 1000, 1000, 0),
            new ChamPionStatsData(20, 15, 3, 0, 10));
        AddChampionTower("cham32",
            new ChamPionProfileData("공격버프챔편", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.ChampionTower, 10000, 5000, 30),
            new ChamPionStatusData(2000, 2000, 2000, 0),
            new ChamPionStatsData(50, 15, 3, 0, 10));
        AddChampionTower("cham33",
            new ChamPionProfileData("공격버프챔편", "none", "Prefabs/Monster/LV1_Golem.prefab", ActorType.ChampionTower, 10000, 5000, 30),
            new ChamPionStatusData(5000, 5000, 3000, 0),
            new ChamPionStatsData(100, 15, 3, 0, 10));

    }
    public SetTowerDatas GetTowerStatusData(string towerID, ActorType type)
    {
        List<TowerEntryDatas> data = null;
        switch (type)
        {
            case ActorType.NormarMonster:
                data = normarTowerEntryDatas;
                break;
            case ActorType.BossMonster:
                data = chapionTowerEntryDatas;
                break;
        }
        foreach (var entry in data)
        {
            if (entry.GetTowerID() == towerID)
            {
                return entry.GetTowerData();
            }
        }
        return null;
    }
}
[Serializable]
public class TowerEntryDatas
{
    public string actorID;
    public SetTowerDatas towerDatas;
    public TowerEntryDatas(string id, SetTowerDatas datas)
    {
        this.actorID = id;
        this.towerDatas = datas;
    }
    public string GetTowerID()
    {
        return actorID;
    }

    public SetTowerDatas GetTowerData()
    {
        return towerDatas;
    }
}
[Serializable]
public class SetTowerDatas
{
    public TowerProfileData Profile;
    public TowerStatusData Status;
    public TowerStatsData Stats;

    public SetTowerDatas(TowerProfileData profile, TowerStatusData status, TowerStatsData stats)
    {
        Profile = profile;
        Status = status;
        Stats = stats;
    }
}
[Serializable]
public class NormarTowerProfileData : TowerProfileData
{
    public NormarTowerProfileData(string name, string iconPath, string prefabPath, ActorType type, int buy, int sell, int upgradeTime)
    {
        this.actorName = name;
        this.iconPath = iconPath;
        this.prefabPath = prefabPath;
        this.type = type;
        this.buyPrice = buy;
        this.sellPrice = sell;
        this.upgradeTime = upgradeTime;
    }
}
[Serializable]
public class NormarTowerStatusData : TowerStatusData
{
    public NormarTowerStatusData(int maxHP, int currentHP, int maxExp, int currentExp)
    {
        this.maxHP = maxHP;
        this.curentHP = currentHP;
        this.maxExp = maxExp;
        this.currentExp = currentExp;
    }
}
[Serializable]
public class NormarTowerStatsData : TowerStatsData
{
    public NormarTowerStatsData(int damage, int range, int attackSpeed, int moveSpeed, float rotationSpeed)
    {
        this.attackDamage = damage;
        this.attackRange = range;
        this.attackSpeed = attackSpeed;
        this.moveSpeed = moveSpeed;
        this.rotationSpeed = rotationSpeed;
    }
}
[Serializable]
public class ChamPionProfileData : TowerProfileData
{
    public ChamPionProfileData(string name, string iconPath, string prefabPath, ActorType type, int buy, int sell, int upgradeTime)
    {
        this.actorName = name;
        this.iconPath = iconPath;
        this.prefabPath = prefabPath;
        this.type = type;
        this.buyPrice = buy;
        this.sellPrice = sell;
        this.upgradeTime = upgradeTime;
    }
}
[Serializable]
public class ChamPionStatusData : TowerStatusData
{
    public ChamPionStatusData(int maxHP, int currentHP, int maxExp,int currentExp)
    {
        this.maxHP = maxHP;
        this.curentHP = currentHP;
        this.maxExp = maxExp;
        this.currentExp = currentExp;
    }
}
[Serializable]
public class ChamPionStatsData : TowerStatsData
{
    public ChamPionStatsData(int damage, int range, int attackSpeed, int moveSpeed, float rotationSpeed)
    {
        this.attackDamage = damage;
        this.attackRange = range;
        this.attackSpeed = attackSpeed;
        this.moveSpeed = moveSpeed;
        this.rotationSpeed = rotationSpeed;
    }
}