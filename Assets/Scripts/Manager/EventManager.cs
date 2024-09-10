using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class EventManager
{
    private static EventManager _instance;
    public static EventManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new EventManager();
            }
            return _instance;
        }
    }
    public event Action<string, string, int> onSpawnMonster;
    public event Action onAllDestoryMonster;
    public event Action onKilledMonster;

    public event Action<TowerData> ontSelectTowerData;
    public event Action onDropTower;
    public event Action onBuyShopTower;
    public void SelectTowerData(TowerData data)
    {
        ontSelectTowerData?.Invoke(data);
    }
    public void StartStage(string prefabPath, string type, int count)
    {
        onSpawnMonster?.Invoke(prefabPath, type, count);
    }
    public void DropTowerForDraggableTowerClearData()
    {
        onDropTower?.Invoke();
    }
    public void BuyShopTower()
    {
        onBuyShopTower?.Invoke();
    }
    public void EndStage()
    {
        onAllDestoryMonster?.Invoke();
    }
    public void KilledMonster()
    {
        onKilledMonster?.Invoke();
    }
}
