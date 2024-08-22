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
    TowerManagerData towerManagerData;
    private void Awake()
    {
        shopData = GameData.instance.shopData;
        towerManagerData = GameData.instance.towerManagerData;
        InitilizedShopUI();
    }
    void InitilizedShopUI()
    {
        for(int i =0; i < shopData.listTowerID.Count; i++)
        {
            ShopSlotUI slotUI = Instantiate(prefab,slotsContents).GetComponent<ShopSlotUI>();
            Button slotButton = slotUI.GetComponent<Button>();
            int index = i;
            slotButton.onClick.AddListener(() => towerManagerData.AddTower(shopData.listTowerID[index], SelectInfo.Shop));
        }
    }
}


