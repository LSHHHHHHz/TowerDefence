using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MonsterSpwaner : MonoBehaviour
{
    [SerializeField] string startColor;
    private int currentSpawnStage;
    private ActorType currentSpawnType;
    private int normarMonsterCount = 10;
    private int bossMonsterCount = 2;
    float spawnTime;

    List<Monster> monsterList = new List<Monster>();
    Coroutine spawnCoroutine;
    private void OnEnable()
    {
       GameManager.instance.stageEventManager.stageEvent += StartSpawnMonster;
    }

    private void OnDisable()
    {
        GameManager.instance.stageEventManager.stageEvent -= StartSpawnMonster;
    }
    public void StartSpawnMonster(int stageNum, string prefabIconPath, ActorType type)
    {
        int maxSpawnCount = 0;
        currentSpawnStage = stageNum;
        currentSpawnType = type;
        switch (type)
        {
            case ActorType.NormarMonster:
                maxSpawnCount = normarMonsterCount;
                spawnTime = 0.1f;
                break;
            case ActorType.BossMonster:
                maxSpawnCount = bossMonsterCount;
                spawnTime = 1f;
                break;
        }
        spawnCoroutine = StartCoroutine(SpawnMonster(prefabIconPath, maxSpawnCount, type));
    }
    IEnumerator SpawnMonster(string prefabIconPath, int spawnCount, ActorType type)
    {
        int count = 0;
        while (count < spawnCount)
        {
            GameObject monsterObj = PoolManager.instance.GetObjectFromPool(prefabIconPath);
            monsterObj.gameObject.SetActive(false);
            Monster monster = monsterObj.GetComponent<Monster>();
            monster.Initialize(currentSpawnStage.ToString(), currentSpawnType);
            if (monster == null)
            {
                Debug.Log("ÇÁ¸®Æé ºÒ·¯¿À±â ¾ÈµÊ");
                yield break;
            }

            MonsterMove monsterMove = monster.GetComponent<MonsterMove>();
            monsterMove.monsterStartColor = startColor;
            monsterObj.gameObject.SetActive(true); 
            monsterList.Add(monster);

            count++;
            yield return new WaitForSeconds(spawnTime);
        }
    }
    public void UnregisterSapwnMonster()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            foreach (Monster monster in monsterList)
            {
                monster.gameObject.SetActive(false);
            }
            monsterList.Clear();
        }
    }
}
