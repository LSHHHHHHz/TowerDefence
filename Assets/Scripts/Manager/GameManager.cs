using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    GameManager instance;
    SpawnManager monsterSpawn;
    private void Awake()
    {
        instance = this;
    }
}
