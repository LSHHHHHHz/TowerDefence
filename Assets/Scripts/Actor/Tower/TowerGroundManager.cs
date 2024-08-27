using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGroundManager : MonoBehaviour
{
    public static TowerGroundManager instance;
    private void Awake()
    {
        instance = this;
        for (int i = 0; i < transform.childCount; i++)
        {
            TowerGround towerGround = transform.GetChild(i).GetComponent<TowerGround>();
            if (towerGround != null && towerGround.gameObject.name == "TowerGround")
            {
                TowerGroundData groundData = new TowerGroundData();
                towerGround.towerGroundData = groundData;
                TowerGroundManagerData.instance.towerGroundDatas.Add(groundData);

                TowerData towerData = new TowerData();
                TowerGroundManagerData.instance.towerGroundDatas[i].towerData = towerData;
                TowerGroundManagerData.instance.towerGroundDatas[i].towerData.towerID = "nor01"; //테스트중
                groundData.setTowerData += towerGround.DropTower;
                groundData.resetTowerData += towerGround.RemoveTower;
            }
        }
    }
}
