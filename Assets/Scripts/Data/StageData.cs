using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class StageData
{
    public int currentStageNumber;
    public int monsterCount;
    public string monsterType;

    public void StageUP()
    {
        currentStageNumber++;
    }
    public void StageDown()
    {
        currentStageNumber--;
    }
}