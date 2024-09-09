using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MonsterSpawner : MonoBehaviour
{
    float monsterSpawnTime;

    List<Monster> monsterList = new List<Monster>();
    Coroutine spawnCoroutine;
    private void OnEnable()
    {
        EventManager.instance.onSpawnMonster += StartSpawnMonster;
        EventManager.instance.onAllDestoryMonster += UnregisterSpawnMonster;
    }

    private void OnDisable()
    {
        EventManager.instance.onSpawnMonster -= StartSpawnMonster;
        EventManager.instance.onAllDestoryMonster -= UnregisterSpawnMonster;
    }
    public void StartSpawnMonster(string prefabIconPath, string type, int count)
    {
        int maxSpawnCount = 0;
        switch (type)
        {
            case "NormarMonster":
                maxSpawnCount = count;
                monsterSpawnTime = 0.1f;
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
                Debug.Log("������ �ҷ����� �ȵ�");
                yield break;
            }

            MonsterMove monsterMove = monster.GetComponent<MonsterMove>();
            //monsterObj.gameObject.SetActive(true); 
            monsterList.Add(monster);

            count++;
            yield return new WaitForSeconds(monsterSpawnTime);
        }
    }
    public void UnregisterSpawnMonster()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            foreach (Monster monster in monsterList)
            {
                if (monster.gameObject.activeSelf)
                {
                    monster.gameObject.SetActive(false);
                }
            }
            monsterList.Clear();
        }
    }
}