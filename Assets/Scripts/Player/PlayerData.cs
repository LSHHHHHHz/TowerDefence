using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerCurrency
{
    public int playerCoin = 0;
    public int playerDia = 1000;
    public void GetCoin(int amount)
    {
        playerCoin += amount;
        EventManager.instance.PlayerCoinChanged(playerCoin);
    }
    public void SpendCoin(int amount)
    {
        playerCoin -= amount;
        EventManager.instance.PlayerCoinChanged(playerCoin);
    }
    public void GetDia(int amount)
    {
        playerDia += amount;
        EventManager.instance.PlayerDiaChanged(playerDia);
    }
    public void SpendDia(int amount)
    {
        playerDia -= amount;
        EventManager.instance.PlayerDiaChanged(playerDia);
    }
}
[Serializable]
public class PlayerStatus
{
    public int playerHP = 30;
    public void GetHP(int amount)
    {
        playerHP += amount;
        EventManager.instance.PlayerHpChanged(playerHP);
    }
    public void ReduceHP()
    {
        playerHP --;
        EventManager.instance.PlayerHpChanged(playerHP);
    }
}