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
    Animator anim;
    public void Start()
    {
        MouseInteraction.instance.selectTowerData += RegisterTowerData;
        EventManager.instance.ontSelectTowerData += RegisterTowerData;
        //마우스쪽은 TowerPlace 알 필요가없어짐
    }
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
