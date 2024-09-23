using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterInEndGate : BaseDetector
{
    Player player;
    ActorManager<Monster> monsterManager;
    private void Awake()
    {
        monsterManager = ActorManager<Monster>.instnace;
        player = GameManager.instance.player;
    }
    protected override void UpdateDetection()
    {
        IReadOnlyList<Monster> actors = monsterManager.GetActors();

        if (actors != null)
        {
            foreach (var actor in actors)
            {
                Vector3 direction = transform.position - actor.transform.position;
                direction.y = 0;

                float distance = direction.magnitude;
                if (distance < detectionRange)
                {
                    EventManager.instance.KilledMonster();
                    if (actor is NormarMonster normarMonster)
                    {
                        player.ReduceHp(1);
                    }
                    if (actor is BossMonster bossMonster)
                    {
                        player.ReduceHp(5);
                    }
                    actor.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }
}