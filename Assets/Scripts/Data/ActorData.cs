using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum ActoryType
{
    NormarMonster,
    BossMonster,
    NormarTower,
    ChampionTower
}
public class ActorProfileData
{
    public string monstName;
    public string iconPath;
    public string prefabPath;
    public ActoryType type;
}
public class ActorStatusData
{
    public int maxHP;
    public int curentHP;
    public int damage;
}
public class MonsterRewardData
{
    public int rewardCoin;
    public int rewardExp;
}

[Serializable]
public class MonsterData
{ 
    public List<EntryDatas> normarMonsterEntryDatas;
    public List<EntryDatas> bossMonsterEntryDatas;
    public MonsterData()
    {
        normarMonsterEntryDatas = new List<EntryDatas>();
        bossMonsterEntryDatas = new List<EntryDatas>();
    }

    public void AddNormarMonster(string monsterID, NormarMonsterProfileData monsterProfileData, NormarMonsterStatusData monsterStatusData, NormarMonsterRewardData monsterRewardData)
    {
        SetMonsterDatas data = new SetMonsterDatas(monsterProfileData, monsterStatusData,monsterRewardData);
        normarMonsterEntryDatas.Add(new EntryDatas(monsterID, data));
    }
    public void AddBossMonster(string monsterID, BossMonsterProfileData monsterProfileData, BossMonsterStatusData monsterStatusData, BossMonsterRewardData monsterRewardData)
    {
        SetMonsterDatas data = new SetMonsterDatas(monsterProfileData, monsterStatusData, monsterRewardData);
        bossMonsterEntryDatas.Add(new EntryDatas(monsterID, data));
    }
    public void InitializeMonsterData()
    {
        AddNormarMonster("노말1", 
            new NormarMonsterProfileData("이름 : 노말몬스터1", "none", "none", ActoryType.NormarMonster),
            new NormarMonsterStatusData(500, 500, 0),
            new NormarMonsterRewardData(10, 10));
        AddNormarMonster("노말2",
            new NormarMonsterProfileData("이름 : 노말몬스터2", "none", "none", ActoryType.NormarMonster),
            new NormarMonsterStatusData(1000, 1000, 0),
            new NormarMonsterRewardData(20, 20));
        AddNormarMonster("노말3",
            new NormarMonsterProfileData("이름 : 노말몬스터3", "none", "none", ActoryType.NormarMonster),
            new NormarMonsterStatusData(1500, 1500, 0),
            new NormarMonsterRewardData(20, 20));
        AddBossMonster("보스1",
           new BossMonsterProfileData("이름 : 보스몬스터1", "none", "none", ActoryType.NormarMonster),
           new BossMonsterStatusData(5000, 5000, 0),
           new BossMonsterRewardData(100, 100));
        AddBossMonster("보스2",
           new BossMonsterProfileData("이름 : 보스몬스터2", "none", "none", ActoryType.NormarMonster),
           new BossMonsterStatusData(8000, 8000, 0),
           new BossMonsterRewardData(200, 200));
    }
}
[Serializable]
public class EntryDatas
{
    public string actorID;
    public SetMonsterDatas monsterDatas;
    public EntryDatas(string id, SetMonsterDatas datas)
    {
        this.actorID = id;
        this.monsterDatas = datas;
    }
}
[Serializable]
public class SetMonsterDatas
{
    public ActorProfileData Profile;
    public ActorStatusData Status;
    public MonsterRewardData Reward;

    public SetMonsterDatas(ActorProfileData profile, ActorStatusData status, MonsterRewardData reward)
    {
        Profile = profile;
        Status = status;
        Reward = reward;
    }
}
[Serializable]
public class NormarMonsterProfileData : ActorProfileData
{
    public NormarMonsterProfileData(string monsterName, string iconPath, string prefabPath, ActoryType type)
    {
        this.monstName = monsterName;
        this.iconPath = iconPath;
        this.prefabPath = prefabPath;
        this.type = type;
    }
}
[Serializable]
public class NormarMonsterStatusData : ActorStatusData
{
    public NormarMonsterStatusData(int maxHP, int currentHP, int damage)
    {
        this.maxHP = maxHP;
        this.curentHP = currentHP;
        this.damage = damage;
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
    public BossMonsterProfileData(string monsterName, string iconPath, string prefabPath, ActoryType type)
    {
        this.monstName = monsterName;
        this.iconPath = iconPath;
        this.prefabPath = prefabPath;
        this.type = type;
    }
}
[Serializable]
public class BossMonsterStatusData : ActorStatusData
{
    public BossMonsterStatusData(int maxHP, int currentHP, int damage)
    {
        this.maxHP = maxHP;
        this.curentHP = currentHP;
        this.damage = damage;
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
[Serializable]
public class NormarTowerData
{

}
[Serializable]
public class ChampionTowerData
{

}
