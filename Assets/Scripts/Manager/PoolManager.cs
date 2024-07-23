using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public List<GameObject> prefabs;
    public List<List<GameObject>> pools;
    private void Awake()
    {
        instance = this; 
        pools = new List<List<GameObject>>();
        for (int i = 0; i < prefabs.Count; i++)
        {
            pools.Add(new List<GameObject>());
        }
    }

    //프리펩아이디로
    public GameObject SetPool(GameObject gameObject)
    {
        int index = -1;
        for (int i = 0; i < prefabs.Count; i++)
        {
            if (prefabs[i] == gameObject)
            {
                index = i;
                break;
            }
        }
        if (index == -1)
        {
            prefabs.Add(gameObject);
            pools.Add(new List<GameObject>());
            index = prefabs.Count - 1;
        }
        GameObject select = null;
        foreach (var obj in pools[index])
        {
            if (!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                return select;
            }
        }
        if (select == null)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        return select;
    }
}
