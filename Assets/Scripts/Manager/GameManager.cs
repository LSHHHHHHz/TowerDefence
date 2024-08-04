using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    GameData gameData;
    public SpawnManager monsterSpawn;
    public InputManager inputManager;
    public StageEventManager stageEventManager;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        gameData = GameData.instance;
        inputManager = new InputManager();
        stageEventManager = new StageEventManager(gameData);
    }
    private void Update()
    {
        inputManager.UpdateInput();
    }
}
