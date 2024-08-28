using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
[Serializable] //유니티에서 보기 위해 사용
public class TowerEventHandler
{
    public TowerGroundData detectedTowerGroundData;
    public TowerData detectedTowerData;

    public event Action<TowerGroundData, TowerData> onSetTowerData;
    public event Action onResetTowerData;
    public event Action<TowerGroundData> onEnterTowerGround;
    public event Action<TowerGroundData> onExitTowerGround;
    public TowerEventHandler()
    {
        detectedTowerGroundData = new TowerGroundData();
        detectedTowerData = new TowerData();
    }
    public void SetTower(TowerGroundData groundData, TowerData towerData)
    {
        groundData.SetTower(towerData);
        onSetTowerData?.Invoke(groundData, towerData);
    }
    public void RemoveTower(TowerGroundData groundData)
    {
        groundData.RemoveTower();
        onResetTowerData?.Invoke();
    }
    public void ChangeTowerPos()
    {

    }
    public void MergeTower()
    {

    }
    public void EnterTowerGround(TowerGroundData data)
    {
        onEnterTowerGround?.Invoke(data);
    }
    public void ExitTowerGround(TowerGroundData data)
    {
        onExitTowerGround?.Invoke(data);
    }
}