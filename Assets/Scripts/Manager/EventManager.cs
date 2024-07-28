using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public GameData gameData;
    public Action<string, ActoryType> stageEvent;
    public Action<SetMonsterDatas> currentStageNormarMonsterHandler;
    public Action<SetMonsterDatas> currentStageBossMonsterHandler;
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
        stageEvent?.Invoke(prefabPath, type);
    }
    public void ResetStageEvent(string stageMonsterId)
    {
        SetMonsterDatas setNormarMonsterDatas = gameData.monsterData.GetMonsterStatusData(stageMonsterId, ActoryType.NormarMonster);
        SetMonsterDatas setBossMonsterDatas = gameData.monsterData.GetMonsterStatusData(stageMonsterId, ActoryType.BossMonster);
        currentStageNormarMonsterHandler?.Invoke(setNormarMonsterDatas);
        currentStageBossMonsterHandler?.Invoke(setBossMonsterDatas);
    }
}
