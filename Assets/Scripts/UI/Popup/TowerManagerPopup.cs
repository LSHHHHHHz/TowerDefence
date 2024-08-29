using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class TowerManagerPopup : MonoBehaviour
{
    [SerializeField] Image towerTypeImage;
    [SerializeField] Text towerTpyeNameText;
    [SerializeField] Text towerLV;
    [SerializeField] Text towerDamageUpPriceTextForCoin;
    [SerializeField] Text towerDamageUPPriceTextForDia;        
    [SerializeField] Text towerSellPrice;
    [SerializeField] Text towerCurrentAttackDamage;        
    [SerializeField] Text towerCurrentAttackSpeed;

    public void SetPopupData(string typePath, string typeName, int lv, int damageUpPriceForCoin, int damageUpPriceForDia, int sellPrice, int currentAttackDamage, int currentAttackSpeed)
    {
        Sprite towerSprite = Resources.Load<Sprite>(typePath);
        if (towerSprite != null)
        {
            towerTypeImage.sprite = towerSprite;
        }
        else
        {
            Debug.LogWarning("경로 없음 " + typePath);
        }
        towerTpyeNameText.text = typeName;                     
        towerLV.text = "LV : " + lv.ToString();                
        towerDamageUpPriceTextForCoin.text = damageUpPriceForCoin.ToString(); 
        towerDamageUPPriceTextForDia.text = damageUpPriceForDia.ToString();  
        towerSellPrice.text =  sellPrice.ToString();    
        towerCurrentAttackDamage.text = "Damage: " + currentAttackDamage.ToString(); 
        towerCurrentAttackSpeed.text = "Speed: " + currentAttackSpeed.ToString();
    }
    public void UpdataPopupData(string typeName, int lv, int damageUpPriceForCoin, int damageUpPriceForDia, int sellPrice, int currentAttackDamage, int currentAttackSpeed)
    {
        towerTpyeNameText.text = typeName;
        towerLV.text = "LV : " + lv.ToString();
        towerDamageUpPriceTextForCoin.text = damageUpPriceForCoin.ToString();
        towerDamageUPPriceTextForDia.text = damageUpPriceForDia.ToString();
        towerSellPrice.text = sellPrice.ToString();
        towerCurrentAttackDamage.text = "Damage: " + currentAttackDamage.ToString();
        towerCurrentAttackSpeed.text = "Speed: " + currentAttackSpeed.ToString();
    }

    public void OpenPopup()
    {
        gameObject.SetActive(true);
    }
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}


