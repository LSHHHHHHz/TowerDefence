using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class ShopPopup : MonoBehaviour
{
    Player player;
    [SerializeField] GameObject prefab;
    [SerializeField] RectTransform slotsContents;
    DraggableTower draggableTower;
    List<ShopDB> shopDB;
    event Action<TowerData> onBuingTower;
    event Action<int> onPlayerBuingTower;
    private void Awake()
    {
        player = GameManager.instance.player;
        shopDB = GameManager.instance.gameEntityData.shopEntity;
        draggableTower = GameManager.instance.draggableTower;
        InitializeShopUI();
    }
    private void OnEnable()
    {
        onBuingTower += draggableTower.GetTower;
        onPlayerBuingTower += player.SpendCoin;
    }
    private void OnDisable()
    {
        onBuingTower -= draggableTower.GetTower;
        onPlayerBuingTower -= player.SpendCoin;
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
                if(player.PlayerHasCoin() - captureData.status.expenseTowerCoin > 0)
                {
                    player.SpendCoin(captureData.status.expenseTowerCoin);
                    onBuingTower?.Invoke(captureData);
                    EventManager.instance.BuyShopTower();
                    ClosePopup();
                }
                else
                {
                    Debug.Log("플레이어 돈 부족");
                }
                ClosePopup();
            });
        }
    }
    void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}