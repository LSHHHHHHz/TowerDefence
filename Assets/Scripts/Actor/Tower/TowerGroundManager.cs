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
                TowerOnGroundData groundData = new TowerOnGroundData();
                groundData.towerID = "";
                groundData.type = ActorType.None;
                towerGround.towerGroundData = groundData;
                TowerGroundManagerData.instance.towerGroundDatas.Add(groundData);
                TowerGroundManagerData.instance.towerGroundDatas[i].SetGroundNumger(i);
                groundData.setTowerData += towerGround.DropTower;
                groundData.resetTowerData += towerGround.RemoveTower;
            }
        }
    }
    public string SetTower(int num)
    {
        for(int i =0; i< TowerGroundManagerData.instance.towerGroundDatas.Count; i++)
        {
            if(TowerGroundManagerData.instance.towerGroundDatas[i].groundNumber == num)
            {
                return TowerGroundManagerData.instance.towerGroundDatas[i].towerID;
            }
        }
        return null;
    }
}
