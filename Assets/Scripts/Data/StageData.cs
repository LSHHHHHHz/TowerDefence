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

    public List<string> normarMonsterPrefabsPath = new List<string>();
    public List<string> bossMonsterPrefabsPath = new List<string>();

    public StageData()
    {
        InitializeStageMonsterData();
    }
    public void InitializeStageMonsterData()
    {
        normarMonsterPrefabsPath.Add("Prefabs/Monster/Normar/LV1_Golem");
        normarMonsterPrefabsPath.Add("Prefabs/Monster/Normar/LV2_Golem");
        normarMonsterPrefabsPath.Add("Prefabs/Monster/Normar/LV3_Golem");
    }
    public string GetMonsterObj(int num, ActorType type)
    {
        List<string> monsterPath = new List<string>();
        switch (type)
        {
            case ActorType.NormarMonster:
                monsterPath = normarMonsterPrefabsPath;
                break;
            case ActorType.BossMonster:
                monsterPath = bossMonsterPrefabsPath;
                break;
        }
        return monsterPath[num-1];
    }
}