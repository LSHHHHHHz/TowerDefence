using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TowerManager : MonoBehaviour 
{
    private const int maxSize = 2;
    Queue<TowerData> towerData = new Queue<TowerData>();
    bool isSelectTower = false;
    public void RegisterTowerData(TowerData data, bool isBuyShop)
    {
        if(isSelectTower)
        {
            return;
        }
        isSelectTower = isBuyShop;
        if (towerData.Count >= maxSize)
        {
            towerData.Dequeue();
        }
        towerData.Enqueue(data);
        Debug.LogError("towerData 개수 : " + towerData.Count);
    }
    public void RefreshTowerData()
    {
        isSelectTower = false;
        towerData.Clear();
    }
    public TowerData GetTowerData()
    {
        if (towerData.Count > 0)
        {
            return towerData.Peek();
        }
        else
        {
            Debug.LogError("타워데이터 없음");
            return null;
        }
    }
}
