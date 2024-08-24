using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager
{
    public Action<string, string> stageEvent;
    public Action<int> checkStageNumEvent; 
    public Action<ProfileDB, MonsterStatusDB> currentStageNormarMonsterEvent;
    public Action<ProfileDB, MonsterStatusDB> currentStageBossMonsterEvent;
    public void StartStageEvent(int stageNum, string type)
    {
        string id = GameManager.instance.gameEntityData.GetMonsterIdByStage(stageNum, type);
        string prefabPath = GameManager.instance.gameEntityData.GetProfileDB(id).prefabPath;
        stageEvent?.Invoke(prefabPath, type);
    }
    public void ResetStageEvent(string stageMonsterId, ActorType type)
    {
        if (type == ActorType.NormarMonster)
        {
            currentStageNormarMonsterEvent?.Invoke(GameManager.instance.gameEntityData.GetProfileDB(stageMonsterId), GameManager.instance.gameEntityData.GetMonsterStatusDB(stageMonsterId));
        }
        if (type == ActorType.BossMonster)
        {
            currentStageBossMonsterEvent?.Invoke(GameManager.instance.gameEntityData.GetProfileDB(stageMonsterId), GameManager.instance.gameEntityData.GetMonsterStatusDB(stageMonsterId));
        }
    }
}
