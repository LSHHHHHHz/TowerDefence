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

    private void Awake()
    {
        InitilizedShopUI();
    }
    void InitilizedShopUI()
    {
        for(int i =0; i < 3; i++)
        {
            ShopSlotUI slotUI = Instantiate(prefab,slotsContents).GetComponent<ShopSlotUI>();
        }
    }
}


