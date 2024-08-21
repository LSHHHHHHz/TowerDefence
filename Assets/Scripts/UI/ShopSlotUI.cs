using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour 
{
    [SerializeField] Image towerImage;
    [SerializeField] Text towerPrice;
    private void Awake()
    {
        
    }
    public void InitializeShopUI(string path, int price)
    {
        towerImage.sprite = Resources.Load<Sprite>(path);
        towerPrice.text = price.ToString();
    }
}
