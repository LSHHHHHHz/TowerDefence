using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[Serializable]
public class TowerGroundData
{
    public TowerData towerData;
    public int towerGroundNum;
    public event Action<TowerGroundData, TowerData> onGround;
    public void SetTowerNum(int towerGroundNum)
    {
        this.towerGroundNum = towerGroundNum;
    }
    public void SetTower(TowerGroundData groundData, TowerData towerData)
    {
        this.towerData = towerData;
        onGround?.Invoke(this, this.towerData);
    }
    public void RemoveTower()
    {
        towerData = null;
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
    public TowerStatusDB status;
    public ActorType type;
    //상태도 여기서 관리할지 추후에 생각필요
    public void UpgradeTower()
    {
        //타워 업그레이드 데이터 갖고있어야함       
    }
}