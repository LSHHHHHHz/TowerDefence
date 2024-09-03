using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TowerPlacementManager
{
    private const int maxSize = 2;
    public List<TowerData> towerData = new List<TowerData>();
    public GameObject buyTowerObj { get; set; }
    public TowerData buyTowerData { get; private set; }
    public event Action clickTwoTower;
    public event Action<bool> canMergeTower;
    public void RegisterTowerData(TowerData data)
    {
        if (towerData.Count >= maxSize)
        {
            towerData.RemoveAt(0);
        }
        if (data != null)
        {
            towerData.Add(data);
        }
        if(towerData.Count == maxSize)
        {
            clickTwoTower?.Invoke();
            bool canMerge = towerData[0] == towerData[1];
            canMergeTower?.Invoke(canMerge);
        }
    }
    public void RefreshTowerData()
    {
        towerData.Clear(); 
    }
    public void BuyTower(GameObject buyTowerObj, TowerData towerData)
    {
        this.buyTowerObj = buyTowerObj;
        this.buyTowerData = towerData;
    }
    public void BuyTowerDropOnGround()
    {
        RefreshTowerData();
        if (buyTowerObj != null)
        {
            buyTowerObj.SetActive(false);
            buyTowerObj = null;
        }
    }
}
