using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StageManager : MonoBehaviour
{
    public event Action<string, string> startStageEvent;
    int currentStage = 1;
    int clearMonsterCount = 0;
    float stageClearAfterElapsedTime = 5f;
    bool isClearStage = false;

    int stageNormarMonsterCount = 100;
    int stageBossMonsterCount = 2;
    int currentStageMonsterCount;
    string currentActorType;
    private void Update()
    {
        CheckStageClear();
    }
    public void StartStage(int stage, string type)
    {
        GameManager.instance.stageEventManager.StartStage(currentStage, currentActorType, currentStageMonsterCount);
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
        StartStage(currentStage, currentActorType);
    }
    void UnRegisterSapwnMonster()
    {

    }
    void ChangeStageInfo(string type)
    {
        currentStage++;
        switch (type)
        {
            case "NormarMonster":
                currentActorType = "BossMonster";
                currentStageMonsterCount = stageBossMonsterCount;
                break;
            case "BossMonster":
                currentActorType = "NormarMonster";
                currentStageMonsterCount = stageNormarMonsterCount;
                break;
        }
    }
}