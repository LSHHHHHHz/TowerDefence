using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public List<GameObject> prefabs;
    public Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();

    private void Awake()
    {
        instance = this;

        foreach (var prefab in prefabs)
        {
            string prefabPath = prefab.name;
            pools[prefabPath] = new List<GameObject>();
            prefabCache[prefabPath] = prefab;
        }
    }
    public GameObject GetObjectFromPool(string gameObjectPath)
    {
        if (!prefabCache.ContainsKey(gameObjectPath))
        {
            GameObject loadedPrefab = Resources.Load<GameObject>(gameObjectPath);
            if (loadedPrefab != null)
            {
                prefabCache[gameObjectPath] = loadedPrefab;
                pools[gameObjectPath] = new List<GameObject>();
            }
            else
            {
                Debug.LogError($"Failed to load prefab at path: {gameObjectPath}");
                return null;
            }
        }

        GameObject selectedObject = null;
        List<GameObject> pool = pools[gameObjectPath];

        foreach (var obj in pool)
        {
            if (!obj.activeSelf)
            {
                selectedObject = obj;
                selectedObject.SetActive(true);
                return selectedObject;
            }
        }
        if (selectedObject == null)
        {
            selectedObject = Instantiate(prefabCache[gameObjectPath], transform);
            pool.Add(selectedObject);
        }

        return selectedObject;
    }
}
