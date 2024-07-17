using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour 
{
    public static PoolManager instance;
    public GameObject[] prefab;
    public List<GameObject>[] pools;

    private void Awake()
    {
        instance = this;
        pools = new List<GameObject>[prefab.Length];
        for(int i = 0; i<pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }
    public GameObject Get(int index)
    {
        GameObject select = null;
        foreach (var obj in pools[index])
        {
            if (!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                break;
            }
        }
        if (select == null)
        {
            select = Instantiate(prefab[index],transform);
            pools[index].Add(select);
        }
        return select;
    }
}
