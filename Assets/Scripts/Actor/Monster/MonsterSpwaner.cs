using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MonsterSpwaner : MonoBehaviour
{
    float monsterSpawnTime;

    List<Monster> monsterList = new List<Monster>();
    Coroutine spawnCoroutine;
    private void OnEnable()
    {
        GameManager.instance.stageEventManager.stageEvent += StartSpawnMonster;
        GameManager.instance.stageEventManager.endStage += UnregisterSapwnMonster;
    }

    private void OnDisable()
    {
        GameManager.instance.stageEventManager.stageEvent -= StartSpawnMonster;
        GameManager.instance.stageEventManager.endStage -= UnregisterSapwnMonster;
    }
    public void StartSpawnMonster(string prefabIconPath, string type, int count)
    {
        int maxSpawnCount = 0;
        switch (type)
        {
            case "NormarMonster":
                maxSpawnCount = count;
                monsterSpawnTime = 0.3f;
                break;
            case "BossMonster":
                maxSpawnCount = count;
                monsterSpawnTime = 1f;
                break;
        }
        spawnCoroutine = StartCoroutine(SpawnMonster(prefabIconPath, maxSpawnCount));
    }
    IEnumerator SpawnMonster(string prefabIconPath, int spawnCount)
    {
        int count = 0;
        while (count < spawnCount)
        {
            GameObject monsterObj = PoolManager.instance.GetObjectFromPool(prefabIconPath);
            //monsterObj.gameObject.SetActive(false);
            Monster monster = monsterObj.GetComponent<Monster>();
            if (monster == null)
            {
                Debug.Log("ÇÁ¸®Æé ºÒ·¯¿À±â ¾ÈµÊ");
                yield break;
            }

            MonsterMove monsterMove = monster.GetComponent<MonsterMove>();
            //monsterObj.gameObject.SetActive(true); 
            monsterList.Add(monster);

            count++;
            yield return new WaitForSeconds(monsterSpawnTime);
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