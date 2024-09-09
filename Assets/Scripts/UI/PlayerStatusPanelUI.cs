using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatusPanelUI : MonoBehaviour
{
    [SerializeField] Text remainMonsterCountText;
    [SerializeField] Text playerCurrentHpText;
    [SerializeField] Text playerCoinText;
    [SerializeField] Text playerDiaText;

    private void OnEnable()
    {
        EventManager.instance.onPlayerHpChanged += UpdateCurrentHpUI;
        EventManager.instance.onPlayerCoinChanged += UpdateCoinUI;
        EventManager.instance.onPlayerDiaChanged += UpdateDiaUI;
        EventManager.instance.onStageMonsterCountChanged += UpdateMonsterCountUI;
        Initialized();
    }
    private void OnDisable()
    {
        EventManager.instance.onPlayerHpChanged -= UpdateCurrentHpUI;
        EventManager.instance.onPlayerCoinChanged -= UpdateCoinUI;
        EventManager.instance.onPlayerDiaChanged -= UpdateDiaUI;
        EventManager.instance.onStageMonsterCountChanged -= UpdateMonsterCountUI;
    }
    private void Initialized()
    {
        UpdateMonsterCountUI(GameManager.instance.stageManager.currentStageMonsterCount);
        UpdateCurrentHpUI(GameManager.instance.player.status.playerHP);
        UpdateCoinUI(GameManager.instance.player.currency.playerCoin);
        UpdateDiaUI(GameManager.instance.player.currency.playerDia);
    }
    public void UpdateMonsterCountUI(int currentMonsterCount)
    {
        remainMonsterCountText.text = currentMonsterCount.ToString();
    }
    public void UpdateCurrentHpUI(int currentHP)
    {
        playerCurrentHpText.text = currentHP.ToString();
    }
    public void UpdateCoinUI(int currentCoin)
    {
        playerCoinText.text = currentCoin.ToString();
    }
    public void UpdateDiaUI(int currentDia)
    {
        playerDiaText.text = currentDia.ToString();
    }
}