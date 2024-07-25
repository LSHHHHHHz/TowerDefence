using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour 
{
    int currentStageNumber = 1;

    int initialStageNumber = 1;
    int maxStageNumber = 999;

    [SerializeField] Text stageText;
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
    }
    public void SelectStage()
    {

    }
}
