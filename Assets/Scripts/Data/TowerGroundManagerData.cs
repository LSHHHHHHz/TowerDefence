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
    //���µ� ���⼭ �������� ���Ŀ� �����ʿ�
    public void UpgradeTower()
    {
        //Ÿ�� ���׷��̵� ������ �����־����       
    }
}