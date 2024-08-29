using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGroundManager : MonoBehaviour
{
    public static TowerGroundManager instance;
    public TowerEventHandler towerEventHandler;
    private void Awake()
    {
        instance = this;
        towerEventHandler = new TowerEventHandler();
        for (int i = 0; i < transform.childCount; i++)
        {
            TowerGround towerGround = transform.GetChild(i).GetComponent<TowerGround>();
            if (towerGround != null && towerGround.gameObject.name == "TowerGround")
            {
                TowerGroundData groundData = new TowerGroundData();
                towerGround.towerGroundData = groundData;
                TowerGroundManagerData.instance.towerGroundDatas.Add(groundData);

                //�׽�Ʈ ��
              //  TowerData towerData = new TowerData();
               // groundData.towerData = towerData;
               // groundData.towerData.towerID = "nor01"; //�׽�Ʈ��
                //�׽�Ʈ ��

                towerEventHandler.onSetTowerData += towerGround.DropTower;
                towerEventHandler.onResetTowerData += towerGround.RemoveTower;
                towerEventHandler.onEnterTowerGround += towerGround.OnEnterGround;
                towerEventHandler.onExitTowerGround += towerGround.OnExitGround;
            }
        }
    }
}
