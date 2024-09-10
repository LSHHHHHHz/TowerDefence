using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TowerGroundManager : MonoBehaviour
{
    public static TowerGroundManager instance;

    MouseInteraction mouseInteraction;

    public event Action<TowerGroundData, TowerData> setTower;
    public event Action removeTower;
    private TowerGround currentSelectedGround;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        mouseInteraction = MouseInteraction.instance;
        for (int i = 0; i < transform.childCount; i++)
        {
            TowerGround towerGround = transform.GetChild(i).GetComponent<TowerGround>();
            if (towerGround != null && towerGround.gameObject.name == "TowerGround")
            {
                TowerGroundData groundData = new TowerGroundData();
                towerGround.towerGroundData = groundData;
                TowerGroundManagerData.instance.towerGroundDatas.Add(groundData);

                mouseInteraction.dropBuyTowerOnGround += towerGround.DropTower;
                mouseInteraction.inMouseOnGround += towerGround.OnEnterGround;
                mouseInteraction.outMouseOnGround += towerGround.OnExitGround;
            }
        }
    }
    public void SetCurrentSelectedGround(TowerGround towerGround)
    {
        currentSelectedGround = towerGround; 
    }
    public bool IsSameGroundSelected(TowerGround towerGround)
    {
        return currentSelectedGround == towerGround; 
    }
    public void ClearData()
    {
        currentSelectedGround = null;
    }
}
