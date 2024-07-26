using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    Monster spawnMonster;

    ActoryType currentMonsterType;
    string currentMonsterId;

    int initialNormarMonsterCount = 0;
    int normarMonsterCount = 100;
    int initialBossMonsterCount = 0;
    int bossMonsterCount = 2;
    private void Start()
    {
        StartCoroutine(SpawnCoroutine(currentMonsterType));
    }
    public void StartSpawn(MonsterData data)
    {
        List<Vector3> redPoint = data.monsterWarePointDatas.GetWarePointPos("Red");
        List<Vector3> bluePoint = data.monsterWarePointDatas.GetWarePointPos("Blue");
    }
    IEnumerator SpawnCoroutine(ActoryType type)
    {
        int spawnCount = 0;
        int count = 0;
        switch (type)
        {
            case ActoryType.NormarMonster:
                spawnCount = initialNormarMonsterCount;
                break;
            case ActoryType.BossMonster:
                spawnCount = initialBossMonsterCount;
                break;

        }
        while (count < spawnCount)
        {

            yield return new WaitForSeconds(0.3f);
        }
    }
    void ResetSpawn()
    {

    }
}
