using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageUI : MonoBehaviour
{
    GameData gameData;

    [SerializeField] Text stageText;
    [SerializeField] StageMonsterInfoUI normarMonsterInfoUI;
    [SerializeField] StageMonsterInfoUI bossMonsterInfoUI;
    [SerializeField] Image selectNormarStageBackGround;
    [SerializeField] Image selectBossStageBackGround;

    ActorType selectMonsterStageType;
    private void Awake()
    {
        gameData = GameData.instance;
        selectMonsterStageType = ActorType.NormarMonster;
        UpdateStageUI();
    }
    private void OnEnable()
    {
        GameManager.instance.stageEventManager.currentStageNormarMonsterEvent += normarMonsterInfoUI.SetMonsterData;
        GameManager.instance.stageEventManager.currentStageBossMonsterEvent += bossMonsterInfoUI.SetMonsterData;
        GameManager.instance.stageEventManager.ResetStageEvent(GameManager.instance.gameEntityData.GetMonsterIdByStage(gameData.stageData.currentStageNumber, ActorType.NormarMonster));
    }
    private void OnDisable()
    {
        GameManager.instance.stageEventManager.currentStageNormarMonsterEvent -= normarMonsterInfoUI.SetMonsterData;
        GameManager.instance.stageEventManager.currentStageBossMonsterEvent -= bossMonsterInfoUI.SetMonsterData;
    }
    public void StageDown()
    {
        if (gameData.stageData.currentStageNumber > gameData.stageData.initialStageNumber)
        {
            gameData.stageData.currentStageNumber--;
            UpdateStageUI();
        }
    }
    public void StageUP()
    {
        if (gameData.stageData.currentStageNumber < gameData.stageData.clearStageNumber)
        {
            gameData.stageData.currentStageNumber++;
            UpdateStageUI();
        }
    }
    void UpdateStageUI()
    {
        stageText.text = "Stage " + gameData.stageData.currentStageNumber.ToString();
        GameManager.instance.stageEventManager.ResetStageEvent(gameData.stageData.currentStageNumber.ToString());
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
        GameManager.instance.stageEventManager.StartStageEvent(gameData.stageData.currentStageNumber, selectMonsterStageType);
    }
}


