using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour 
{
    int currentStageNumber = 1;
    int lastStageNumber = 1;

    int initialStageNumber = 1;
    int maxStageNumber = 999;

    [SerializeField] Text stageText;
    [SerializeField] MonsterInfoUI normarMonsterInfoUI;
    [SerializeField] MonsterInfoUI bossMonsterInfoUI;
    GameData gameData;
    SetMonsterDatas setNormarMonsterDatas;
    SetMonsterDatas setBossMonsterDatas;
    private void Awake()
    {
        currentStageNumber = lastStageNumber;
        gameData = GameData.instance;
        stageText.text = "Stage " + currentStageNumber.ToString();
        SetMonsterData();
        SetMonsterInfoUI();
    }
    public void StageDown()
    {
        if (currentStageNumber -1 == initialStageNumber) 
        {
            return;
        }
        else
        {
            currentStageNumber--;
        }
        stageText.text = "Stage " + currentStageNumber.ToString();
        SetMonsterData();
        SetMonsterInfoUI();
    }
    public void StageUP()
    {
        if (currentStageNumber + 1 > maxStageNumber)
        {
            return;
        }
        else
        {
            currentStageNumber++;
        }
        stageText.text = "Stage " + currentStageNumber.ToString();
        SetMonsterData();
        SetMonsterInfoUI();
    }
    private void SetMonsterData()
    {
        setNormarMonsterDatas = gameData.monsterData.GetMonsterStatusData("노말몬스터" + lastStageNumber.ToString(),ActoryType.NormarMonster);
        setBossMonsterDatas = gameData.monsterData.GetMonsterStatusData("보스몬스터" + lastStageNumber.ToString(),ActoryType.BossMonster);
    }
    private void SetMonsterInfoUI()
    {
        normarMonsterInfoUI.SetMonsterData(setNormarMonsterDatas);
        bossMonsterInfoUI.SetMonsterData(setBossMonsterDatas);
    }
    public void SelectStage()
    {

    }
}
