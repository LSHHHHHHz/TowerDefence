using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDebuff
{
    public List<int> slowDebuffList = new List<int>();
    public int currentSlowDebuff { get; private set; } = 1;

    public void AddSlowDebuff(int amount)
    {
        if (!slowDebuffList.Contains(amount))
        {
            slowDebuffList.Add(amount);
        }
        UpdateCurrentSlowDebuff();
    }
    public void RemoveSlowDebuff(int amount)
    {
        if (slowDebuffList.Contains(amount))
        {
            slowDebuffList.Remove(amount);
        }
        UpdateCurrentSlowDebuff();
    }
    public void ClearDebuffs()
    {
        slowDebuffList.Clear();
        currentSlowDebuff = 1;
    }
    private void UpdateCurrentSlowDebuff()
    {
        if (slowDebuffList.Count > 0)
        {
            int maxSlow = slowDebuffList[0]; 
            for (int i = 1; i < slowDebuffList.Count; i++)
            {
                if (slowDebuffList[i] > maxSlow)
                {
                    maxSlow = slowDebuffList[i];
                }
            }
            currentSlowDebuff = maxSlow;
        }
        else
        {
            currentSlowDebuff = 1; 
        }
    }
}