using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class ShopUIPopup : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] RectTransform slotsContents;
    TowerPlacementManager towerManager;
    List<ShopDB> shopDB;
    event Action<GameObject, TowerData> buyTowerObject;
    event Action isBuingTower;
    private void Awake()
    {
        shopDB = GameManager.instance.gameEntityData.shopEntity;
        towerManager = GameData.instance.towerManager;
        InitializeShopUI();
    }
    private void OnEnable()
    {
        buyTowerObject += towerManager.BuyTower;
        isBuingTower += MouseInteraction.instance.BuingTower;
    }
    private void OnDisable()
    {
        buyTowerObject -= towerManager.BuyTower;
        isBuingTower -= MouseInteraction.instance.BuingTower;
    }
    void InitializeShopUI()
    {
        for (int i = 0; i < shopDB.Count; i++)
        {
            ShopSlotUI slotUI = Instantiate(prefab, slotsContents).GetComponent<ShopSlotUI>();
            TowerData data = new TowerData();
            data.towerID = shopDB[i].dataID;
            data.status = GameManager.instance.gameEntityData.GetTowerStatusDB(shopDB[i].dataID);
            data.type = GameManager.instance.gameEntityData.GetActorType(GameManager.instance.gameEntityData.GetTowerStatusDB(shopDB[i].dataID).type);
            TowerData captureData = data;
            slotUI.InitializeShopUI(shopDB[i].iconPath, shopDB[i].price);
            string prfabPath = shopDB[i].iconPath;
            Button slotButton = slotUI.GetComponent<Button>();
            slotButton.onClick.AddListener(() =>
            {
                //여기서 마우스 인터렉션이랑 상호작용 필요
                InstantiateObj(prfabPath, captureData);
                isBuingTower?.Invoke();
                ClosePopup();
            });
        }
    }
    void InstantiateObj(string path, TowerData data)
    {
        GameObject obj = PoolManager.instance.GetObjectFromPool(path);
        buyTowerObject?.Invoke(obj, data);
    }
    void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}