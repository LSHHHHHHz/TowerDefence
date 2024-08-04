using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager
{
    public GameData gameData;
    public Action<int,string, ActorType> stageEvent;
    public Action<int> checkStageNumEvent; 
    public Action<SetMonsterDatas> currentStageNormarMonsterEvent;
    public Action<SetMonsterDatas> currentStageBossMonsterEvent;
    public StageEventManager(GameData gameData)
    {
        this.gameData = gameData;
    }
    public void StartStageEvent(int stageNum, ActorType type)
    {
        string prefabPath = gameData.stageData.GetMonsterObj(stageNum, type);
        stageEvent?.Invoke(stageNum,prefabPath, type);
    }
    public void ResetStageEvent(string stageMonsterId)
    {
        SetMonsterDatas setNormarMonsterDatas = gameData.monsterData.GetMonsterStatusData(stageMonsterId, ActorType.NormarMonster);
        SetMonsterDatas setBossMonsterDatas = gameData.monsterData.GetMonsterStatusData(stageMonsterId, ActorType.BossMonster);
        currentStageNormarMonsterEvent?.Invoke(setNormarMonsterDatas);
        currentStageBossMonsterEvent?.Invoke(setBossMonsterDatas);
    }
}
