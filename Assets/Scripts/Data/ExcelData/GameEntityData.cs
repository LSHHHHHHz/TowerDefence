using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class GameEntityData : ScriptableObject
{
	public List<ProfileDB> profileEntity;
	public List<ActorStatusDB> actorStatusEntity; 

	public ProfileDB GetProfileData(string id)
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
	public ActorStatusDB GetActorStatusDB(string id)
	{
		foreach (ActorStatusDB actor in actorStatusEntity)
		{
			if (id == actor.dataID)
			{
				return actor;
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
