using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MonsterSpwaner : MonoBehaviour
{
    private int _normarMonsterCount;
    public int normarMonsterCount
    {
        get { return 100; }
        set { _normarMonsterCount = value; }
    }
    private int _bossMonsterCount;
    public int bossMonsterCount
    {
        get { return 100; }
        set { _bossMonsterCount = value; }
    }
    float spawnTime;

    List<Monster> monsterList = new List<Monster>();
    Coroutine spawnCoroutine;
    private void OnEnable()
    {
        EventManager.instance.stageEvent += StartSpawnMonster;
    }

    private void OnDisable()
    {
        EventManager.instance.stageEvent -= StartSpawnMonster;
    }
    public void StartSpawnMonster(string prefabIconPath, ActoryType type)
    {
        Debug.Log("여기 스폰 되나");
        int maxSpawnCount = 0;
        switch (type)
        {
            case ActoryType.NormarMonster:
                maxSpawnCount = normarMonsterCount;
                spawnTime = 0.1f;
                break;
            case ActoryType.BossMonster:
                maxSpawnCount = bossMonsterCount;
                spawnTime = 1f;
                break;
        }
        spawnCoroutine = StartCoroutine(SpawnMonster(prefabIconPath, maxSpawnCount, type));
    }
    IEnumerator SpawnMonster(string prefabIconPath, int spawnCount, ActoryType type)
    {
        int count = 0;
        if(PoolManager.instance.GetObjectFromPool(prefabIconPath).GetComponent<Monster>() == null)
        {
            Debug.Log("프리펩 불러오기 안됨");
            yield break;
        }
        Monster monster = PoolManager.instance.GetObjectFromPool(prefabIconPath).GetComponent<Monster>();
        while (count < spawnCount)
        {
            monsterList.Add(monster);
            yield return new WaitForSeconds(spawnTime);
        }
    }
    public void UnregisterMonster()
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
