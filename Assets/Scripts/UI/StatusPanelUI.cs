using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusPanelUI : MonoBehaviour
{
    [SerializeField] Text remainMonsterCountText;
    [SerializeField] Text playerCurrentHpText;
    [SerializeField] Text playerCoinText;
    [SerializeField] Text playerDiaText;

    private void OnEnable()
    {
        GameData.instance.playerStatus.playerHpChanged += UpdateCurrentHpUI;
        GameData.instance.playerCurrency.coinChanged += UpdateCoinUI;
        GameData.instance.playerCurrency.diaChanged += UpdateDiaUI;
        Initialized();
    }
    private void OnDisable()
    {
        GameData.instance.playerStatus.playerHpChanged -= UpdateCurrentHpUI;
        GameData.instance.playerCurrency.coinChanged -= UpdateCoinUI;
        GameData.instance.playerCurrency.diaChanged -= UpdateDiaUI;
    }
    private void Initialized()
    {
        UpdateMonsterCountUI();
        UpdateCurrentHpUI();
        UpdateCoinUI();
        UpdateDiaUI();
    }
    public void UpdateMonsterCountUI()
    {
        remainMonsterCountText.text = 11.ToString();
    }
    public void UpdateCurrentHpUI()
    {
        playerCurrentHpText.text = GameData.instance.playerStatus.BringPlayerHP().ToString();
    }
    public void UpdateCoinUI()
    {
        playerCoinText.text = GameData.instance.playerCurrency.BringPlayerCoin().ToString();
    }
    public void UpdateDiaUI()
    {
        playerDiaText.text = GameData.instance.playerCurrency.BringPlayerDia().ToString();
    }
}