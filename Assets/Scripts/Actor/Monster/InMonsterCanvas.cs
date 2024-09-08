using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.SceneManagement;
using UnityEngine;

public class InMonsterCanvas : MonoBehaviour
{
    public Action<float, float> updateHpBar;
    HPBar hpBar;
    Coin coinImage;
    private void Awake()
    {
        hpBar = GetComponentInChildren<HPBar>();
        coinImage = GetComponentInChildren<Coin>();      
        updateHpBar += hpBar.UpdateFillAmount;
        Initialized();
    }
    private void Initialized()
    {
        if (coinImage != null)
        {
            coinImage.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("코인이미지 없음");
        }
    }
    public void OnEnableCoin()
    {
        coinImage.gameObject.SetActive(true);
    }
    public void OnDisableCoin()
    {
        coinImage.gameObject.SetActive(false);
    }
}
