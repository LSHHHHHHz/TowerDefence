using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    GameData gameData;
    int currentStageNumber = 1;
    int initialStageNumber = 1;
    public int clearStageNumber = 3;

    [SerializeField] Text stageText;
    [SerializeField] StageMonsterInfoUI normarMonsterInfoUI;
    [SerializeField] StageMonsterInfoUI bossMonsterInfoUI;
    [SerializeField] Image selectNormarStageBackGround;
    [SerializeField] Image selectBossStageBackGround;

    SetMonsterDatas setNormarMonsterDatas;
    SetMonsterDatas setBossMonsterDatas;
    ActorType selectMonsterStageType;

    private void Awake()
    {
        gameData = GameData.instance;
        currentStageNumber = gameData.stageData.lastStage;
        stageText.text = "Stage " + currentStageNumber.ToString();
    }
    private void OnEnable()
    {
        GameManager.instance.stageEventManager.currentStageNormarMonsterEvent += normarMonsterInfoUI.SetMonsterData;
        GameManager.instance.stageEventManager.currentStageBossMonsterEvent += bossMonsterInfoUI.SetMonsterData;
        GameManager.instance.stageEventManager.ResetStageEvent(currentStageNumber.ToString());
    }
    private void OnDisable()
    {
        GameManager.instance.stageEventManager.currentStageNormarMonsterEvent -= normarMonsterInfoUI.SetMonsterData;
        GameManager.instance.stageEventManager.currentStageBossMonsterEvent -= bossMonsterInfoUI.SetMonsterData;
    }
    public void StageDown()
    {
        currentStageNumber--;
        if (currentStageNumber < initialStageNumber)
        {
            currentStageNumber++;
            return;
        }
        stageText.text = "Stage " + currentStageNumber.ToString();
        GameManager.instance.stageEventManager.ResetStageEvent(currentStageNumber.ToString());
    }
    public void StageUP()
    {
        currentStageNumber++;
        if (currentStageNumber > clearStageNumber)
        {
            currentStageNumber--;
            return;
        }
        stageText.text = "Stage " + currentStageNumber.ToString();
        GameManager.instance.stageEventManager.ResetStageEvent(currentStageNumber.ToString());
    }
    public void SelectNormarStage()
    {
        selectNormarStageBackGround.enabled = true;
        selectBossStageBackGround.enabled = false;
        selectMonsterStageType = ActorType.NormarMonster;
    }
    public void SelectBossStage()
    {
        selectNormarStageBackGround.enabled = false;
        selectBossStageBackGround.enabled = true;
        selectMonsterStageType = ActorType.BossMonster;
    }
    public void StartStage()
    {
        GameManager.instance.stageEventManager.StartStageEvent(currentStageNumber, selectMonsterStageType);
    }
}
