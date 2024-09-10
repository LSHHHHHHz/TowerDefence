using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DraggableTower : MonoBehaviour
{
    MouseInteraction mouseInteraction;
    TowerData dragTowerData;
    GameObject dragTowerObj;
    Action<GameObject, TowerData> onDragTowerObj;
    private void Awake()
    {
        mouseInteraction = GameManager.instance.mouseInteraction;
    }
    private void OnEnable()
    {
        EventManager.instance.onDropTower += ResetTower;
        onDragTowerObj += mouseInteraction.DragTowerObj;
    }
    private void OnDisable()
    {
        EventManager.instance.onDropTower -= ResetTower;
        onDragTowerObj -= mouseInteraction.DragTowerObj;
    }
    public void GetTower(TowerData data)
    {
        dragTowerData = data;
        dragTowerObj = PoolManager.instance.GetObjectFromPool(GameManager.instance.gameEntityData.GetProfileDB(dragTowerData.towerID).prefabPath);
        onDragTowerObj?.Invoke(dragTowerObj, dragTowerData);
    }
    public void ResetTower()
    {
        dragTowerData = null;
        onDragTowerObj?.Invoke(null, dragTowerData);
        dragTowerObj.SetActive(false);
    }
}