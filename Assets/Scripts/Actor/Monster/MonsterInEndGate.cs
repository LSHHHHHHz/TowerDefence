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
                EventManager.instance.KilledMonster();
                if (monster is NormarMonster normarMonster)
                {
                    player.ReduceHp(1);
                }
                if(monster is BossMonster bossMonster)
                {
                    player.ReduceHp(5);
                }
                monster.gameObject.SetActive(false);
            }
        }
    }
}
