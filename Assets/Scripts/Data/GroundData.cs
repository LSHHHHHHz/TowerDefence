using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class TowerOnGroundData 
{
    public string towerID;
    public ActorType type;
    public event Action resetTowerData;
    public event Action<string,ActorType> setTowerData;
    public void SetTower(string  towerID , ActorType type)
    {
        this.towerID = towerID;
        this.type = type;
        setTowerData?.Invoke(towerID, type);
    }
    public void RemoveTower()
    {
        towerID = null;
        resetTowerData?.Invoke();
    }
}
[Serializable]
public class GroundData 
{
    [SerializeField] private List<TowerOnGroundData> _towerGroundData;
    public GroundData()
    {
        InitializeSlots(58);
    }
    public List<TowerOnGroundData> towerGroundData
    {
        get
        {
            return _towerGroundData;
        }
        private set
        {
            _towerGroundData = value;
        }
    }
    public void InitializeSlots(int count)
    {
        towerGroundData = new List<TowerOnGroundData>(count);
        for (int i = 0; i < count; i++)
        {
            towerGroundData.Add(new TowerOnGroundData());
        }
    }
}