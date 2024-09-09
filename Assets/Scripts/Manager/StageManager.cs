using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StageManager : MonoBehaviour
{
    StageDB stageDB;
    int currentStage = 1;
    float stageClearAfterElapsedTime = 5f;
    bool isClearStage = false;
    bool startStage =false;
    int clearMonsterCount = 0;
    public int currentStageMonsterCount { get; private set; }
    string currentActorType = "NormarMonster";

    public Action<int, string,int,int,string,int,int> onInitializedStage;
    public Action<string> onUpdateCurrentMonsterTypeIconPath;
    public Action<int> onUpdateCurrentMonsterCount;
    private void Start()
    {

    }
    private void Update()
    {
        if (startStage)
        {
            CheckStageClear();
        }
    }
    private void OnEnable()
    {
        EventManager.instance.onKilledMonster += UpdateCurrentMonsterCount;
    }
    private void OnDisable()
    {
        EventManager.instance.onKilledMonster -= UpdateCurrentMonsterCount;
    }
    public void StartStage()
    {
        startStage = true;
        stageDB = GameManager.instance.gameEntityData.GetStageDB(currentStage, currentActorType);

        string id = GameManager.instance.gameEntityData.GetMonsterIdByStage(stageDB.stage, stageDB.type);
        string prefabPath = GameManager.instance.gameEntityData.GetProfileDB(id).prefabPath;

        currentStageMonsterCount = stageDB.spawnCount;
        EventManager.instance.StartStage(prefabPath, stageDB.type, currentStageMonsterCount);
        EventManager.instance.StageMonsterCountChanged(currentStageMonsterCount);
    }
    void CheckStageClear()
    {
        if (currentStageMonsterCount <= clearMonsterCount && !isClearStage)
        {
            isClearStage = true;
            StartCoroutine(HandleStageClear());
        }
    }
    IEnumerator HandleStageClear()
    {
        Debug.LogError("스테이지 클리어!");

        yield return new WaitForSeconds(stageClearAfterElapsedTime);

        StartNextStage();
    }
    void StartNextStage()
    {
        isClearStage = false;
        Debug.LogError("다음 스테이지 시작!");
        ChangeStageInfo(currentActorType);
        EventManager.instance.EndStage();
        StartStage();
    }
    public void UpdateCurrentMonsterCount()
    {
        currentStageMonsterCount--;
        EventManager.instance.StageMonsterCountChanged(currentStageMonsterCount);        
    }
    void ChangeStageInfo(string type)
    {
        switch (type)
        {
            case "NormarMonster":
                currentActorType = "BossMonster";
                break;
            case "BossMonster":
                currentActorType = "NormarMonster";
                currentStage++;
                break;
        }
    }
}