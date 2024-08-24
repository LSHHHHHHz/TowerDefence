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

    string selectMonsterStageType;
    private void Awake()
    {
        gameData = GameData.instance;
        selectMonsterStageType = "NormarMonster";
    }
    private void OnEnable()
    {
        GameManager.instance.stageEventManager.currentStageNormarMonsterEvent += normarMonsterInfoUI.SetMonsterData;
        GameManager.instance.stageEventManager.currentStageBossMonsterEvent += bossMonsterInfoUI.SetMonsterData;
        UpdateStageUI();
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
            gameData.stageData.StageDown();
            UpdateStageUI();
        }
    }
    public void StageUP()
    {
        if (gameData.stageData.currentStageNumber < gameData.stageData.clearStageNumber)
        {
            gameData.stageData.StageUP();
            UpdateStageUI();
        }
    }
    void UpdateStageUI()
    {
        stageText.text = "Stage " + gameData.stageData.currentStageNumber.ToString();
        GameManager.instance.stageEventManager.ResetStageEvent(GameManager.instance.gameEntityData.GetMonsterIdByStage(gameData.stageData.currentStageNumber, "NormarMonster"),ActorType.NormarMonster);
        GameManager.instance.stageEventManager.ResetStageEvent(GameManager.instance.gameEntityData.GetMonsterIdByStage(gameData.stageData.currentStageNumber, "BossMonster"),ActorType.BossMonster);
    }
    public void SelectNormarStage()
    {
        selectNormarStageBackGround.enabled = true;
        selectBossStageBackGround.enabled = false;
        selectMonsterStageType = "NormarMonster";
    }
    public void SelectBossStage()
    {
        selectNormarStageBackGround.enabled = false;
        selectBossStageBackGround.enabled = true;
        selectMonsterStageType = "BossMonster";
    }
    public void StartStage()
    {
        GameManager.instance.stageEventManager.StartStageEvent(gameData.stageData.currentStageNumber, selectMonsterStageType);
    }
}


