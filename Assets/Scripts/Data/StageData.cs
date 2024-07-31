using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class StageData
{
    public int lastStage = 1;
    public List<string> normarMonsterPrefabsPath = new List<string>();
    public List<string> bossMonsterPrefabsPath = new List<string>();

    public StageData()
    {
        InitializeStageMonsterData();
    }
    public void InitializeStageMonsterData()
    {
        normarMonsterPrefabsPath.Add("Prefabs/Monster/LV1_Golem");
        normarMonsterPrefabsPath.Add("Prefabs/Monster/LV2_Golem");
        normarMonsterPrefabsPath.Add("Prefabs/Monster/LV3_Golem");
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