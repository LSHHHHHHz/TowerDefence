using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public PlayerCurrency currency { get; private set; }
    public PlayerStatus status { get; private set; }
    private void Awake()
    {
        InitializedPlayer();
    }
    private void OnEnable()
    {
        status.onPlayerDie += GameManager.instance.gameOverManager.DeathPlayer;
    }
    private void OnDisable()
    {
        status.onPlayerDie -= GameManager.instance.gameOverManager.DeathPlayer;
    }
    private void InitializedPlayer()
    {
        currency = GameData.instance.playerCurrency;
        status = GameData.instance.playerStatus;
    }
    public void GetCoin(int amount)
    {
        currency.GetCoin(amount);
    }
    public void SpendCoin(int amount)
    {
        currency.SpendCoin(amount);
    }
    public int GetPlayerHasCoin()
    {
        return currency.playerCoin;
    }
    public void GetDia(int amount)
    {
        currency.GetDia(amount);
    }
    public void SpendDia(int amount)
    {
        currency.SpendDia(amount);
    }
    public int GetPlayerHasdDia()
    {
        return currency.playerDia;
    }
    public void GetHP(int amount)
    {
        status.GetHP(amount);
    }
    public void ReduceHp(int amount)
    {
        status.ReduceHP(amount);
    }
}
