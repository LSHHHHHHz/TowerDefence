using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TowerDragHandler : MonoBehaviour
{
    TowerManagerData towerManagerData;
    bool isTowerSelected = false;
    Tower tower;
    private void Awake()
    {
        towerManagerData = GameData.instance.towerManagerData;
    }
    private void Update()
    {
        
    }
    private void OnEnable()
    {
        towerManagerData.handlerTower += OnTowerSelected;
    }
    private void OnDisable()
    {
        towerManagerData.handlerTower -= OnTowerSelected;
    }
    void OnTowerSelected(string towerId, SelectInfo info)
    {
        Debug.Log("타워 데이터 선택됨");
        string path = GameData.instance.towerData.GetTowerData(towerId).Profile.prefabPath;
        if(info == SelectInfo.Shop)
        {
            tower = PoolManager.instance.GetObjectFromPool(path).GetComponent<Tower>();
        }
        else if(info == SelectInfo.Field)
        {

        }
        else
        {
            return;
        }

        DragTower();
    }
    void DragTower()
    {
        Debug.Log("타워 이동 중");
    }
}