using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TowerGroundManager : MonoBehaviour
{
    public static TowerGroundManager instance;

    MouseInteraction mouseInteraction;

    public event Action<TowerGroundData, TowerData> setTower;
    public event Action removeTower;
    public TowerGroundData detectedTowerGroundData = new TowerGroundData();
    public TowerGroundData firstSelectedTowerGroundData = new TowerGroundData();
    public TowerGroundData secondSelectTowerGroundData = new TowerGroundData();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        mouseInteraction = MouseInteraction.instance;
        mouseInteraction.selectFirstTowerGround += RegisterTowerData;
        for (int i = 0; i < transform.childCount; i++)
        {
            TowerGround towerGround = transform.GetChild(i).GetComponent<TowerGround>();
            if (towerGround != null && towerGround.gameObject.name == "TowerGround")
            {
                TowerGroundData groundData = new TowerGroundData();
                towerGround.towerGroundData = groundData;
                TowerGroundManagerData.instance.towerGroundDatas.Add(groundData);

                mouseInteraction.dropBuyTower += towerGround.DropTower;
                mouseInteraction.dropBuyTower += SetTower;
                mouseInteraction.inMouseOnGround += towerGround.OnEnterGround;
                mouseInteraction.outMouseOnGround += towerGround.OnExitGround;
            }
        }
    }
    public void SetTower(TowerGroundData groundData, TowerData towerData)
    {
        groundData.SetTower(towerData);
    }
    public void RemoveTower(TowerGroundData groundData)
    {
        groundData.RemoveTower();
        removeTower?.Invoke();
    }
    public void RegisterTowerData(TowerGroundData data)
    {
        if (!string.IsNullOrEmpty(data.towerData.towerID))
        {
            firstSelectedTowerGroundData = data;
        }
        else
        {
            Debug.LogError("여기서 하... 어떻게하지 하호 ㅠ");
            return;
        }
    }
    public void UnregisterTowerData()
    {
        firstSelectedTowerGroundData = null;
        secondSelectTowerGroundData = null;
    }
}
