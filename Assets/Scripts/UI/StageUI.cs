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

    public int clearStageNumber = 3;

    [SerializeField] Text stageText;
    [SerializeField] MonsterInfoUI normarMonsterInfoUI;
    [SerializeField] MonsterInfoUI bossMonsterInfoUI;
    GameData gameData;
    SetMonsterDatas setNormarMonsterDatas;
    SetMonsterDatas setBossMonsterDatas;
    public Action<string> selectStage;
    private void Awake()
    {
        currentStageNumber = lastStageNumber;
        gameData = GameData.instance;
        SetMonsterData();
        SetMonsterInfoUI();
    }
    public void StageDown()
    {
        currentStageNumber--;
        if (currentStageNumber < initialStageNumber)
        {
            currentStageNumber++;
            return;
        }
        SetMonsterData();
        SetMonsterInfoUI();

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
        SetMonsterData();
        SetMonsterInfoUI();

    }
    private void SetMonsterData()
    {
        setNormarMonsterDatas = gameData.monsterData.GetMonsterStatusData("노말몬스터" + currentStageNumber.ToString(), ActoryType.NormarMonster);
        setBossMonsterDatas = gameData.monsterData.GetMonsterStatusData("보스몬스터" + currentStageNumber.ToString(), ActoryType.BossMonster);
    }
    private void SetMonsterInfoUI()
    {
        stageText.text = "Stage " + currentStageNumber.ToString();
        normarMonsterInfoUI.SetMonsterData(setNormarMonsterDatas);
        bossMonsterInfoUI.SetMonsterData(setBossMonsterDatas);
    }
    public void SelectNormarStage()
    {

    }
    public void SelectBossStage()
    {

    }
}
