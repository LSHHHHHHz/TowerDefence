using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatusPanelUI : MonoBehaviour
{
    Player player;
    [SerializeField] Text playerCurrentHpText;
    [SerializeField] Text playerCoinText;
    [SerializeField] Text playerDiaText;
    private void Awake()
    {
        player = GameManager.instance.player;
    }
    private void OnEnable()
    {
        player.status.onPlayerHPChanged += UpdateCurrentHpUI;
        player.currency.onPlayerCoinChanged += UpdateCoinUI;
        player.currency.onPlayerDiaChanged += UpdateDiaUI;
        Initialized();
    }
    private void OnDisable()
    {
        player.status.onPlayerHPChanged -= UpdateCurrentHpUI;
        player.currency.onPlayerCoinChanged -= UpdateCoinUI;
        player.currency.onPlayerDiaChanged -= UpdateDiaUI;
    }
    private void Initialized()
    {
        UpdateCurrentHpUI(GameManager.instance.player.status.playerHP);
        UpdateCoinUI(GameManager.instance.player.currency.playerCoin);
        UpdateDiaUI(GameManager.instance.player.currency.playerDia);
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