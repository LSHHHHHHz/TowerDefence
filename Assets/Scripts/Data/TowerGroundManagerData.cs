using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[Serializable]
public class TowerGroundData
{
    public TowerData towerData;
    public event Action resetTowerData;
    public event Action<TowerData> setTowerData;
    public event Action<TowerGroundData> enterTowerGround;
    public event Action<TowerGroundData> exitTowerGround;
    public void SetTower(TowerData towerData)
    {
        this.towerData = towerData;
        setTowerData?.Invoke(this.towerData);
    }
    public void RemoveTower()
    {
        towerData = null;
        resetTowerData?.Invoke();
    }
    public void EnterTowerGround(TowerGroundData data)
    {
        enterTowerGround?.Invoke(data);
    }
    public void ExitTowerGround(TowerGroundData data)
    {
        exitTowerGround?.Invoke(data);
    }

    public void ChangeTowerPosition(TowerGroundData dragTowerData, TowerGroundData dropTowerData)
    {
    }
    public void MergeTower()
    {

    }
}
[Serializable]
public class TowerGroundManagerData 
{
    public List<TowerGroundData> towerGroundDatas;
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
        towerGroundDatas = new List<TowerGroundData>();
    }
}
[Serializable]
public class TowerData
{
    public string towerID;
    public int level;
    public ActorType type;
    //상태도 여기서 관리할지 추후에 생각필요
    public void UpgradeTower()
    {
        //타워 업그레이드 데이터 갖고있어야함       
    }
}