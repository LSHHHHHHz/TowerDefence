using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StageManager : MonoBehaviour
{
    StageDB currentStageDB;
    StageDB currentNormarMonsterDB;
    StageDB currentBossMonsterDB;
    public CountDownPopup countDownPopup;
    int currentStage = 1;
    float stageClearAfterElapsedTime = 5f;
    bool isClearStage = false;
    bool startStage =false;
    bool possibleStartStage = true;
    int clearMonsterCount = 0;
    public int currentStageMonsterCount { get; private set; }
    string currentActorType = "NormarMonster";

    public Action<int, string,int,int,string,int,int> onInitializedStage;
    public Action<string> onUpdateCurrentMonsterTypeIconPath;
    public Action<int> onUpdateCurrentMonsterCount;
    private void Awake()
    {
        currentStageDB = GameManager.instance.gameEntityData.GetStageDB(currentStage, currentActorType);
        currentNormarMonsterDB = GameManager.instance.gameEntityData.GetStageDB(currentStage, "NormarMonster");
        currentBossMonsterDB = GameManager.instance.gameEntityData.GetStageDB(currentStage, "BossMonster");
        countDownPopup = FindObjectOfType<CountDownPopup>();
    }
    private void Start()
    {
        onInitializedStage?.Invoke(currentStage, currentNormarMonsterDB.iconPath, currentNormarMonsterDB.monsterHP, currentNormarMonsterDB.rewardCoin,
                                    currentBossMonsterDB.iconPath, currentBossMonsterDB.monsterHP, currentBossMonsterDB.rewardCoin);
        onUpdateCurrentMonsterTypeIconPath?.Invoke(currentStageDB.iconPath);
    }
    private void Update()
    {
        if (startStage && possibleStartStage)
        {
            CheckStageClear();
        }
    }
    private void OnEnable()
    {
        EventManager.instance.onKilledMonster += UpdateCurrentMonsterCount;
        countDownPopup.onPossibleNextStage += StartNextStage;
    }
    private void OnDisable()
    {
        EventManager.instance.onKilledMonster -= UpdateCurrentMonsterCount;
        countDownPopup.onPossibleNextStage -= StartNextStage;
    }
    public void StartStage()
    {
        startStage = true;
        currentNormarMonsterDB = GameManager.instance.gameEntityData.GetStageDB(currentStage, "NormarMonster");
        currentBossMonsterDB = GameManager.instance.gameEntityData.GetStageDB(currentStage, "BossMonster");
        onInitializedStage?.Invoke(currentStage, currentNormarMonsterDB.iconPath, currentNormarMonsterDB.monsterHP, currentNormarMonsterDB.rewardCoin,
                                   currentBossMonsterDB.iconPath, currentBossMonsterDB.monsterHP, currentBossMonsterDB.rewardCoin);

        currentStageDB = GameManager.instance.gameEntityData.GetStageDB(currentStage, currentActorType);
        string id = GameManager.instance.gameEntityData.GetMonsterIdByStage(currentStageDB.stage, currentStageDB.type);
        string prefabPath = GameManager.instance.gameEntityData.GetProfileDB(id).prefabPath;

        currentStageMonsterCount = currentStageDB.spawnCount;

        EventManager.instance.StartStage(prefabPath, currentStageDB.type, currentStageMonsterCount);
        onUpdateCurrentMonsterCount?.Invoke(currentStageMonsterCount);
        onUpdateCurrentMonsterTypeIconPath?.Invoke(currentStageDB.iconPath);
    }
    void CheckStageClear()
    {
        if (currentStageMonsterCount <= clearMonsterCount && !isClearStage)
        {
            isClearStage = true;
            countDownPopup.gameObject.SetActive(true);
        }
    }
    void StartNextStage()
    {
        Debug.Log("다음 스테이지 시작!");
        ChangeStageInfo(currentActorType);
        EventManager.instance.EndStage();
        StartStage();
        isClearStage = false;
    }
    public void ReStartStage()
    {
        Debug.Log("스테이지 재시작!");
        EventManager.instance.EndStage();
        StartStage();
        isClearStage = false;
    }
    public void UpdateCurrentMonsterCount()
    {
        currentStageMonsterCount--;
        onUpdateCurrentMonsterCount?.Invoke(currentStageMonsterCount);
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
    public void PossibleStartStage(bool possible)
    {
        possibleStartStage = possible;
    }
}