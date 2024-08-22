using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class TowerGroundData
{
    public List<TowerGround> towerGrounds;

    public TowerGroundData()
    {
        towerGrounds = new List<TowerGround>();
    }

    public TowerGround GetTowerGround(int num)
    {
        for (int i = 0; i < towerGrounds.Count; i++)
        {
            if (i == num)
            {
                return towerGrounds[i];
            }
        }
        return null;
    }
}