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
   // public Dictionary<string, SetMonsterDatas> normarMonsterDatas;
   // public Dictionary<string, SetMonsterDatas> bossMonsterDatas;

    public List<EntryDatas> normarMonsterEntryDatas;
    public List<EntryDatas> bossMonsterEntryDatas;
    public MonsterData()
    {
       // normarMonsterDatas = new Dictionary<string, SetMonsterDatas>();
       // bossMonsterDatas = new Dictionary<string, SetMonsterDatas>();

        normarMonsterEntryDatas = new List<EntryDatas>();
        bossMonsterEntryDatas = new List<EntryDatas>();
    }

    public void AddNormarMonster(string monsterID, NormarMonsterProfileData monsterProfileData, NormarMonsterStatusData monsterStatusData, NormarMonsterRewardData monsterRewardData)
    {
        SetMonsterDatas data = new SetMonsterDatas(monsterProfileData, monsterStatusData,monsterRewardData);
        //normarMonsterDatas[monsterID] = data;

        normarMonsterEntryDatas.Add(new EntryDatas(monsterID, data));
    }
    public void AddBossMonster(string monsterID, BossMonsterProfileData monsterProfileData, BossMonsterStatusData monsterStatusData, BossMonsterRewardData monsterRewardData)
    {

        SetMonsterDatas data = new SetMonsterDatas(monsterProfileData, monsterStatusData, monsterRewardData);
        //bossMonsterDatas[monsterID] = data;
        bossMonsterEntryDatas.Add(new EntryDatas(monsterID, data));
    }
}
[Serializable]
public class EntryDatas
{
    public string actorID;
    SetMonsterDatas monsterDatas;
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

}
[Serializable]
public class BossMonsterStatusData : ActorStatusData
{

}
[Serializable]
public class BossMonsterRewardData : MonsterRewardData

{

}
[Serializable]
public class NormarTowerData
{

}
[Serializable]
public class ChampionTowerData
{

}
