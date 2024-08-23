using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class GameEntityData : ScriptableObject
{
	public List<ProfileDB> profileEntity;
	public List<TowerStatusDB> towerStatusEntity;
	public List<MonsterStatusDB> monsterStatusEntity;

	public ProfileDB GetProfileDB(string id)
	{
		foreach (ProfileDB profile in profileEntity)
		{
			if(id == profile.dataID)
			{
				return profile;
			}
		}
		Debug.LogError("�´� ���̵� ����");
		return null;
    }
	public TowerStatusDB GetTowerStatusDB(string id)
	{
		foreach (TowerStatusDB tower in towerStatusEntity)
		{
			if (id == tower.dataID)
			{
				return tower;
			}
		}
        Debug.LogError("�´� ���̵� ����");
        return null;
    }
    public MonsterStatusDB GetMonsterStatusDB(string id)
    {
        foreach (MonsterStatusDB monster in monsterStatusEntity)
        {
            if (id == monster.dataID)
            {
                return monster;
            }
        }
        Debug.LogError("�´� ���̵� ����");
        return null;
    }
    public string GetMonsterIdByStage(int stage, ActorType type)
    {
        foreach (MonsterStatusDB monster in monsterStatusEntity)
        {
            if(stage == monster.stage && type == ActorType.NormarMonster)
            {
                return monster.dataID;
            }
        }
        Debug.LogError("�´� ���̵� ����");
        return null;
    }
    public ActorType GetActorType(string type)
	{
        if (type == "NormarTower")
        {
            return ActorType.NormarTower;
        }
        if (type == "ChampTower")
        {
            return ActorType.ChampionTower;
        }
        if (type == "NormarMonster")
        {
            return ActorType.NormarMonster;
        }
        if (type == "BossMonster")
        {
            return ActorType.BossMonster;
        }
        Debug.LogError("�´� Ÿ�� ����");
        return ActorType.None;
    }
}
