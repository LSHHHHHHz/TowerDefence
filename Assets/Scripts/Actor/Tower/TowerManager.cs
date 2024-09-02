using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TowerManager
{
    private const int maxSize = 2;
    public List<TowerData> towerData = new List<TowerData>();

    public GameObject buyTowerObj { get; set; }
    public TowerData buyTowerData { get; private set; }
    public event Action<GameObject, TowerData> onTowerBuy;
    public void RegisterTowerData(TowerData data)
    {
        if (towerData.Count >= maxSize)
        {
            towerData.RemoveAt(0);
        }
        towerData.Add(data);
        Debug.LogError("towerData 개수 : " + towerData.Count);
    }
    public void RefreshTowerData()
    {
        towerData.Clear(); 
    }
    public void BuyTower(GameObject buyTowerObj, TowerData towerData)
    {
        this.buyTowerObj = buyTowerObj;
        this.buyTowerData = towerData;
        onTowerBuy?.Invoke(buyTowerObj, buyTowerData);
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
    public TowerData GetTowerData()
    {
        if (towerData.Count > 0)
        {
            return towerData[0]; 
        }
        else
        {
            Debug.LogError("타워데이터 없음");
            return null;
        }
    }
}
