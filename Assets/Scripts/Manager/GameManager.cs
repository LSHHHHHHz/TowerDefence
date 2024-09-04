using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    GameData gameData;
    public GameEntityData gameEntityData;
    public StageEventManager stageEventManager;
    public MonsterSpwaner monsterSpwaner;
    public Player player;
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
        stageEventManager = new StageEventManager();
    }
}
