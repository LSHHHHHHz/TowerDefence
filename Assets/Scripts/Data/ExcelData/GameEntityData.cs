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
    public List<StageDB> stageEntity;
    public List<ShopDB> shopEntity;
    public ProfileDB GetProfileDB(string id)
    {
        foreach (ProfileDB profile in profileEntity)
        {
            if (id == profile.dataID)
            {
                return profile;
            }
        }
        Debug.LogError("맞는 아이디 없음");
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
        Debug.LogError("맞는 아이디 없음");
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
        Debug.LogError("맞는 아이디 없음");
        return null;
    }
    public StageDB GetStageDB(int stageNum, string type)
    {
        foreach(StageDB stage in stageEntity)
        {
            if(stageNum == stage.stage && type == stage.type)
            {
                return stage;
            }
        }
        Debug.LogError("맞는 스테이지 데이터 없음");
        return null;
    }
    public string GetMonsterIdByStage(int stage, string type)
    {
        foreach (MonsterStatusDB monster in monsterStatusEntity)
        {
            if (stage == monster.stage && type == monster.type)
            {
                return monster.dataID;
            }
        }
        Debug.LogError("맞는 아이디 없음");
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
        Debug.LogError("맞는 타입 없음");
        return ActorType.None;
    }
}
