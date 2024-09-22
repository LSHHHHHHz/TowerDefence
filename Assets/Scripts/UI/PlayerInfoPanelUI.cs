using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInfoPanelUI : MonoBehaviour
{
    Player player;
    [SerializeField] Image playerCurrentHpImage;
    [SerializeField] Image playerCoinImage;
    [SerializeField] Image playerDiaImage;

    [SerializeField] Text playerCurrentHpText;
    [SerializeField] Text playerCoinText;
    [SerializeField] Text playerDiaText;
    private void Awake()
    {
        player = GameManager.instance.player;
    }
    private void OnEnable()
    {
        player.status.onPlayerHPChanged += UpdateCurrentHpText;
        player.currency.onPlayerCoinChanged += UpdateCoinText;
        player.currency.onPlayerDiaChanged += UpdateDiaText;
        Initialized();
    }
    private void OnDisable()
    {
        player.status.onPlayerHPChanged -= UpdateCurrentHpText;
        player.currency.onPlayerCoinChanged -= UpdateCoinText;
        player.currency.onPlayerDiaChanged -= UpdateDiaText;
    }
    private void Initialized()
    {
        UpdateCurrentHpText(GameManager.instance.player.status.playerHP);
        UpdateCoinText(GameManager.instance.player.currency.playerCoin);
        UpdateDiaText(GameManager.instance.player.currency.playerDia);
    }
    void UpdateCurrentHpText(int currentHP)
    {
        playerCurrentHpText.text = currentHP.ToString();
        ImageEffect(playerCurrentHpImage);
    }
    void UpdateCoinText(int currentCoin)
    {
        playerCoinText.text = currentCoin.ToString();
        ImageEffect(playerCoinImage);
    }
    void UpdateDiaText(int currentDia)
    {
        playerDiaText.text = currentDia.ToString();
        ImageEffect(playerDiaImage);
    }
    void ImageEffect(Image image)
    {
        image.transform.localScale = Vector3.one;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(image.transform.DOScale(transform.localScale * 1.2f, 0.1f));
        sequence.Append(image.transform.DOScale(1, 0.1f));
    }
}