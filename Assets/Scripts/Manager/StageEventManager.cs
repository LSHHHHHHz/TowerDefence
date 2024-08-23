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
    public Action<ProfileDB, MonsterStatusDB> currentStageNormarMonsterEvent;
    public Action<ProfileDB, MonsterStatusDB> currentStageBossMonsterEvent;
    public StageEventManager(GameData gameData)
    {
        this.gameData = gameData;
    }
    public void StartStageEvent(int stageNum, ActorType type)
    {
        string id = GameManager.instance.gameEntityData.GetMonsterIdByStage(stageNum, type);
        string prefabPath = gameData.stageData.GetMonsterObj(stageNum, type);
        stageEvent?.Invoke(stageNum,prefabPath, type);
    }
    public void ResetStageEvent(string stageMonsterId)
    {
        currentStageNormarMonsterEvent?.Invoke(GameManager.instance.gameEntityData.GetProfileDB(stageMonsterId), GameManager.instance.gameEntityData.GetMonsterStatusDB(stageMonsterId));
        currentStageBossMonsterEvent?.Invoke(GameManager.instance.gameEntityData.GetProfileDB(stageMonsterId), GameManager.instance.gameEntityData.GetMonsterStatusDB(stageMonsterId));
    }
}
