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
    private ActoryType currentSpawnType;
    private int _normarMonsterCount;
    public int normarMonsterCount
    {
        get { return 10; }
        set { _normarMonsterCount = value; }
    }
    private int _bossMonsterCount;
    public int bossMonsterCount
    {
        get { return 2; }
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
    public void StartSpawnMonster(int stageNum, string prefabIconPath, ActoryType type)
    {
        int maxSpawnCount = 0;
        currentSpawnStage = stageNum;
        currentSpawnType = type;
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
