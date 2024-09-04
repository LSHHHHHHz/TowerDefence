using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public PlayerCurrency currency { get; private set; }
    public PlayerStatus status { get; private set; }
    private void Awake()
    {
        InitializedPlayer();
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
    public void GetDia(int amount)
    {
        currency.GetDia(amount);
    }
    public void SpendDia(int amount)
    {
        currency.SpendDia(amount);
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
