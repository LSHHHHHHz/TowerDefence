using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class StageData
{
    public int initialStageNumber = 1;
    public int lastStageNumber = 10;

    public int currentStageNumber = 1;
    public int clearStageNumber = 3;

    public void StageUP()
    {
        currentStageNumber++;
    }
    public void StageDown()
    {
        currentStageNumber--;
    }
}