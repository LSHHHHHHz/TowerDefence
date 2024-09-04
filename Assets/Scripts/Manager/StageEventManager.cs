using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager
{
    public Action<string, string, int> stageEvent;
    public Action endStage;
    public void StartStage(int stageNum, string type , int count)
    {
        string id = GameManager.instance.gameEntityData.GetMonsterIdByStage(stageNum, type);
        string prefabPath = GameManager.instance.gameEntityData.GetProfileDB(id).prefabPath;
        stageEvent?.Invoke(prefabPath, type, count);
    }
    public void EndStage()
    {
        endStage?.Invoke();
    }
}
