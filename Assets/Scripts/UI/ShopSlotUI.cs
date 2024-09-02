using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour 
{
    [SerializeField] Image towerImage;
    [SerializeField] Text towerPrice;
    public TowerData towerData;
    private void Awake()
    {
        
    }
    public void InitializeShopUI(string towerImagePath, int towerPrice)
    {
        towerImage.sprite = Resources.Load<Sprite>(towerImagePath);
        this.towerPrice.text = towerPrice.ToString();
    }
}
