using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이벤트 삭제 및 string 변경
public class StageEventManager
{
    public GameData gameData;
    public Action<int,string, ActorType> stageEvent;
    public Action<int> checkStageNumEvent; 
    public Action<string> currentStageNormarMonsterEvent;
    public Action<string> currentStageBossMonsterEvent;
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
        currentStageNormarMonsterEvent?.Invoke(stageMonsterId);
        currentStageBossMonsterEvent?.Invoke(stageMonsterId);
    }
}
