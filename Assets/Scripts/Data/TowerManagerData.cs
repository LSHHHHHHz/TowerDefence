using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum SelectInfo
{
    None,
    Field,  
    Shop
}
[Serializable]
public class TowerManagerData
{
    public string towerID;
    public SelectInfo selectInfo;
    public event Action<string, SelectInfo> handlerTower;
    public void AddTower(string towerId, SelectInfo info)
    {
        towerID = towerId;
        selectInfo = info;
        handlerTower?.Invoke(towerID, selectInfo);
    }
    public void RemoveTower()
    {
        if (towerID != null)
        {
            towerID = null;
            selectInfo = SelectInfo.None;
        }
    }
}