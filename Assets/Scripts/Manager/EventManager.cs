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
    public event Action<bool> onPossibleStartStage;

    public event Action onAllDestoryMonster;
    public event Action onKilledMonster;

    public event Action onSetTower;
    public event Action onBuyShopTower;

    public event Action onClickUpgradeButton;
    public void StartStage(string prefabPath, string type, int count)
    {
        onSpawnMonster?.Invoke(prefabPath, type, count);
    }
    public void ActiveAttack()
    {
        onSetTower?.Invoke();
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
    public void ClickUpgradeButton()
    {
        onClickUpgradeButton?.Invoke();
    }
}
