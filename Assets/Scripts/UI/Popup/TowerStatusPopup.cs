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
    [SerializeField] Text towerCurrentAttackDamage;        
    [SerializeField] Text towerCurrentAttackSpeed;
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
    public void UpdatePopupData(TowerData data)
    {
        Sprite towerSprite = Resources.Load<Sprite>(data.status.towerTypePath);
        if (towerSprite != null)
        {
            towerTypeImage.sprite = towerSprite;
        }
        else
        {
            Debug.LogWarning("경로 없음 " + data.status.towerTypePath);
        }
        towerTpyeNameText.text = data.status.name;                     
        towerLV.text = "LV : " + data.status.level.ToString(); 
        towerCurrentAttackDamage.text = "Damage: " + data.status.attackDamage.ToString(); 
        towerCurrentAttackSpeed.text = "Speed: " + data.status.attackSpeed.ToString();
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