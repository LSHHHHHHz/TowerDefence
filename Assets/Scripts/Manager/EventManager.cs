using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public GameData gameData;
    public Action<int,string, ActoryType> stageEvent;
    public Action<int> checkStageNumEvent; 
    public Action<SetMonsterDatas> currentStageNormarMonsterEvent;
    public Action<SetMonsterDatas> currentStageBossMonsterEvent;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        gameData = GameData.instance;
    }
    public void StartStageEvent(int stageNum, ActoryType type)
    {
        string prefabPath = gameData.stageData.GetMonsterObj(stageNum, type);
        stageEvent?.Invoke(stageNum,prefabPath, type);
    }
    public void ResetStageEvent(string stageMonsterId)
    {
        SetMonsterDatas setNormarMonsterDatas = gameData.monsterData.GetMonsterStatusData(stageMonsterId, ActoryType.NormarMonster);
        SetMonsterDatas setBossMonsterDatas = gameData.monsterData.GetMonsterStatusData(stageMonsterId, ActoryType.BossMonster);
        currentStageNormarMonsterEvent?.Invoke(setNormarMonsterDatas);
        currentStageBossMonsterEvent?.Invoke(setBossMonsterDatas);
    }
}
