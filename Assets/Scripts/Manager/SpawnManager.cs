using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager
{
    public string objID;
    public Vector3 spawnPos;
    public Quaternion spawnRot;
    public SpawnManager(string id, Vector3 pos, Quaternion rot )
    {
        GameObject obj = PoolManager.instance.GetObjectFromPool(id);
        if (obj != null)
        {
            obj.transform.position = pos;
            obj.transform.rotation = rot;
        }
    }
    public void Spawn(string id, Vector3 pos)
    {
        GameObject obj = PoolManager.instance.GetObjectFromPool(id);
        if (obj != null)
        {
            obj.transform.position = pos;
        }
    }
}
