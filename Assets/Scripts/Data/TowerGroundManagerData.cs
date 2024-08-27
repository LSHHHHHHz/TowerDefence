using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class TowerOnGroundData 
{
    public int groundNumber;
    public string towerID;
    public ActorType type;
    public event Action resetTowerData;
    public event Action<string,ActorType> setTowerData;
    public void SetGroundNumger(int groundNumber)
    {
        this.groundNumber = groundNumber;
    }
    public int GetGroundNumber()
    {
        return groundNumber;
    }
    public void SetTower(string  towerID , ActorType type)
    {
        this.towerID = towerID;
        this.type = type;
        setTowerData?.Invoke(this.towerID, this.type);
    }
    public void RemoveTower()
    {
        towerID = null;
        resetTowerData?.Invoke();
    }
    public void ChangeTowerPosition(TowerOnGroundData dragTower, TowerOnGroundData dropTower)
    {
        TowerOnGroundData temp = null;
        temp = dragTower;
        dragTower = dropTower;
        dropTower = temp;
        dragTower.SetTower(dragTower.towerID, dragTower.type);
        dropTower.SetTower(dropTower.towerID, dropTower.type);
    }
    public void ChangeTowerPosition2(TowerOnGroundData dragTower, TowerOnGroundData dropTower)
    {
        string tempTowerID = dragTower.towerID;
        ActorType tempType = dragTower.type;

        dragTower.towerID = dropTower.towerID;
        dragTower.type = dropTower.type;
        dragTower.setTowerData?.Invoke(dragTower.towerID, dragTower.type);

        dropTower.towerID = tempTowerID;
        dropTower.type = tempType;
        dropTower.setTowerData?.Invoke(dropTower.towerID, dropTower.type);
    }
}
[Serializable]
public class TowerGroundManagerData 
{
    public List<TowerOnGroundData> towerGroundDatas;
    private static TowerGroundManagerData _instance;

    public static TowerGroundManagerData instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TowerGroundManagerData();
            }
            return _instance;
        }
    }
    private TowerGroundManagerData()
    {
        towerGroundDatas = new List<TowerOnGroundData>();
    }
}