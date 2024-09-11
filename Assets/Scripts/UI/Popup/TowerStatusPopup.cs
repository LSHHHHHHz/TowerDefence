using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class TowerStatusPopup : MonoBehaviour
{
    RectTransform rectTransform;
    Vector2 originPos = new Vector2(0, -280);
    Vector2 targetPos = new Vector2(0, 0);
    float elapsedTime = 0f;
    float duration =0.3f;
    bool isPossibleMerge = false;

    [SerializeField] Image towerTypeImage;
    [SerializeField] Text towerTpyeNameText;
    [SerializeField] Text towerLV;
    [SerializeField] Text towerDamageUpPriceTextForCoin;
    [SerializeField] Text towerDamageUPPriceTextForDia;        
    [SerializeField] Text towerSellPrice;
    [SerializeField] Text towerCurrentAttackDamage;        
    [SerializeField] Text towerCurrentAttackSpeed;
    [SerializeField] Image possibleTowerMergeImage;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        rectTransform.anchoredPosition = originPos;
        elapsedTime = 0;
        StartCoroutine (ActiveTowerPopup());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
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
    public void UpdatePossibleMergeTower(bool possible)
    {
        if (possible)
        {
            possibleTowerMergeImage.color = new Color(0, 0, 1);
        }
        else
        {
            possibleTowerMergeImage.color = new Color(1, 0, 0);
        }
        isPossibleMerge = possible;
    }
    public void ClickMergeTower()
    {
        if(isPossibleMerge)
        {
            //타워 부분에다가 이벤트 처리
        }
        else
        {
            //삐삐 소리
        }
    }
    IEnumerator ActiveTowerPopup()
    {
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPos, t);
            yield return null;
        }
        rectTransform.anchoredPosition = targetPos;
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