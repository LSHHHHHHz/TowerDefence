using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInEndGate : MonoBehaviour
{
    Player player;
    private void Awake()
    {
        player = GameManager.instance.player;   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster != null)
            {
                Debug.LogError("MonsterEndGate");
                EventManager.instance.KilledMonster();
                player.ReduceHp(1);
                monster.gameObject.SetActive(false);
            }
        }
    }
}
