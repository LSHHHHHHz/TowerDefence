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
    ActoryType selectMonsterStageType;

    private void Awake()
    {
        gameData = GameData.instance;
        currentStageNumber = gameData.stageData.lastStage;
        stageText.text = "Stage " + currentStageNumber.ToString();
    }
    private void OnEnable()
    {
        EventManager.instance.currentStageNormarMonsterHandler += normarMonsterInfoUI.SetMonsterData;
        EventManager.instance.currentStageBossMonsterHandler += bossMonsterInfoUI.SetMonsterData;
        EventManager.instance.ResetStageEvent(currentStageNumber.ToString());
    }
    private void OnDisable()
    {
        EventManager.instance.currentStageNormarMonsterHandler -= normarMonsterInfoUI.SetMonsterData;
        EventManager.instance.currentStageBossMonsterHandler -= bossMonsterInfoUI.SetMonsterData;
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
        EventManager.instance.ResetStageEvent(currentStageNumber.ToString());
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
        EventManager.instance.ResetStageEvent(currentStageNumber.ToString());
    }
    public void SelectNormarStage()
    {
        selectNormarStageBackGround.enabled = true;
        selectBossStageBackGround.enabled = false;
        selectMonsterStageType = ActoryType.NormarMonster;
    }
    public void SelectBossStage()
    {
        selectNormarStageBackGround.enabled = false;
        selectBossStageBackGround.enabled = true;
        selectMonsterStageType = ActoryType.BossMonster;
    }
    public void StartStage()
    {
        EventManager.instance.StartStageEvent(currentStageNumber, selectMonsterStageType);
    }
}
