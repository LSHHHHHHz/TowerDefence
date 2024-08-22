using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class GameData
{
    private static GameData _instance;
    public static GameData instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameData();
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    public StageData stageData;
    public MonsterData monsterData;
    public TowerData towerData;
    public MonsterWarePointData monsterWarePointData;
    public TowerGroundManagerData groundData;
    public TowerShopData shopData;
    public TowerManagerData towerManagerData;
    public TowerGroundData towerGroundData;
    public GameData()
    {
        stageData = new StageData();
        monsterData = new MonsterData();
        towerData = new TowerData();
        groundData = new TowerGroundManagerData();
        shopData = new TowerShopData();
        towerManagerData = new TowerManagerData();
        towerGroundData = new TowerGroundData();
        Save();
    }
    [ContextMenu("Save To Json Data")]
    public void Save()
    {
        string jsonData = JsonUtility.ToJson(this, true);
        string path = Path.Combine(Application.dataPath, "UserData.json");
        File.WriteAllText(path, jsonData);
    }
    [ContextMenu("Load From Json Data")]
    public void Load()
    {
        string path = Path.Combine(Application.dataPath, "UserData.json");
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(jsonData, instance);
        }
    }
}