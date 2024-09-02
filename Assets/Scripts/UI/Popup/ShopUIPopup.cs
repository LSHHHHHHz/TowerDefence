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
    TowerShopData shopData;
    TowerManager towerManager;
    private void Awake()
    {
        shopData = GameData.instance.shopData;
        towerManager = GameData.instance.towerManager;
        InitializeShopUI();
    }
    void InitializeShopUI()
    {
        for(int i =0; i < shopData.listTowerID.Count; i++)
        {
            ShopSlotUI slotUI = Instantiate(prefab,slotsContents).GetComponent<ShopSlotUI>();
            TowerData data = new TowerData();
            data.towerID = shopData.listTowerID[i];
            data.status = GameManager.instance.gameEntityData.GetTowerStatusDB(shopData.listTowerID[i]);
            data.type = GameManager.instance.gameEntityData.GetActorType(GameManager.instance.gameEntityData.GetTowerStatusDB(shopData.listTowerID[i]).type);
            TowerData captureData = data;
            slotUI.InitializeShopUI(GameManager.instance.gameEntityData.GetProfileDB(shopData.listTowerID[i]).iconPath, GameManager.instance.gameEntityData.GetProfileDB(shopData.listTowerID[i]).buyTowerPrice);
            Button slotButton = slotUI.GetComponent<Button>();
            slotButton.onClick.AddListener(() =>
            {
                towerManager.RefreshTowerData();
                towerManager.RegisterTowerData(captureData, true);
                ClosePopup();
            });
        }
    }
    void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}