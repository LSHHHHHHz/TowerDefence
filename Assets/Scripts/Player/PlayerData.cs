using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerCurrency
{
    int playerCoin = 0;
    int playerDia = 1000;
    public event Action coinChanged;
    public event Action diaChanged;
    public void GetCoin(int amount)
    {
        playerCoin += amount;
        coinChanged?.Invoke();
    }
    public void SpendCoin(int amount)
    {
        playerCoin -= amount;
        coinChanged?.Invoke();
    }
    public int BringPlayerCoin()
    {
        return playerCoin;
    }
    public void GetDia(int amount)
    {
        playerDia += amount;
        diaChanged?.Invoke();  
    }
    public void SpendDia(int amount)
    {
        playerDia -= amount;
        diaChanged?.Invoke();
    }
    public int BringPlayerDia()
    {
        return playerDia;
    }

}
[Serializable]
public class PlayerStatus
{
    int playerHP = 30;
    public event Action playerHpChanged;
    public void GetHP(int amount)
    {
        playerHP += amount;
        playerHpChanged?.Invoke();
    }
    public void ReduceHP(int amount)
    {
        playerHP -= amount;
        playerHpChanged?.Invoke();
    }
    public int BringPlayerHP()
    {
        return playerHP;
    }
}