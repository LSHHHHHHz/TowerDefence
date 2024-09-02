using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGroundManager : MonoBehaviour
{
    public static TowerGroundManager instance;

    [SerializeField] MouseInteraction mouseInteraction;

    public event Action<TowerGroundData, TowerData> setTower;
    public event Action removeTower;

    public TowerGroundData detectedTowerGroundData;
    public TowerGroundData selectedTowerGroundData;
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

                setTower += towerGround.DropTower;
                removeTower += towerGround.RemoveTower;
                mouseInteraction.inMouseOnGround += towerGround.OnEnterGround;
                mouseInteraction.outMouseOnGround+= towerGround.OnExitGround;
            }
        }
    }
    public void SetTower(TowerGroundData groundData, TowerData towerData)
    {
        groundData.SetTower(towerData);
        setTower?.Invoke(groundData, towerData);
    }
    public void RemoveTower(TowerGroundData groundData)
    {
        groundData.RemoveTower();
        removeTower?.Invoke();
    }
    public void RegisterTowerData()
    {

    }
    public void UnregisterTowerData()
    {

    }
}
