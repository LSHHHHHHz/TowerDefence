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
    DraggableTower draggableTower;
    List<ShopDB> shopDB;
    event Action<TowerData> isBuingTower;
    private void Awake()
    {
        shopDB = GameManager.instance.gameEntityData.shopEntity;
        draggableTower = GameManager.instance.draggableTower;
        InitializeShopUI();
    }
    private void OnEnable()
    {
        isBuingTower += draggableTower.GetTower;
    }
    private void OnDisable()
    {
        isBuingTower -= draggableTower.GetTower;
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
                isBuingTower?.Invoke(captureData);
                EventManager.instance.BuyShopTower();
                ClosePopup();
            });
        }
    }
    void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}