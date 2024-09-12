using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerCurrency
{
    public int playerCoin = 500;
    public int playerDia = 1000;
    public event Action<int> onPlayerCoinChanged;
    public event Action<int> onPlayerDiaChanged;
    public void GetCoin(int amount)
    {
        playerCoin += amount;
        onPlayerCoinChanged?.Invoke(playerCoin);
    }
    public void SpendCoin(int amount)
    {
        playerCoin -= amount;
        onPlayerCoinChanged?.Invoke(playerCoin);
    }
    public void GetDia(int amount)
    {
        playerDia += amount;
        onPlayerDiaChanged?.Invoke(playerDia);
    }
    public void SpendDia(int amount)
    {
        playerDia -= amount;
        onPlayerDiaChanged?.Invoke(playerDia);
    }
}
[Serializable]
public class PlayerStatus
{
    public int playerHP = 30;
    public Action<int> onPlayerHPChanged;
    public void GetHP(int amount)
    {
        playerHP += amount;
        onPlayerHPChanged?.Invoke(playerHP);
    }
    public void ReduceHP(int amount)
    {
        playerHP -= amount;
        onPlayerHPChanged?.Invoke(playerHP);
    }
}