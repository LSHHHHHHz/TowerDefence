using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGroundManager : MonoBehaviour
{
    TowerGroundManagerData groundData;
    List<TowerGround> towerGroundList = new List<TowerGround>();
    private void Awake()
    {
        groundData = GameData.instance.groundData;
        for (int i = 0; i < transform.childCount; i++)
        {
            TowerGround towerGround = transform.GetChild(i).GetComponent<TowerGround>();
            towerGroundList.Add(towerGround);
            groundData.towerGroundDatas[i].setTowerData += towerGround.DropTower;
            groundData.towerGroundDatas[i].resetTowerData += towerGround.RemoveTower;
        }
    }
}
