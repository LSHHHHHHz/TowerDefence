using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameData gameData;
    public GameEntityData gameEntityData;
    public StageManager stageManager;
    public DraggableTower draggableTower;
    public MouseInteraction mouseInteraction;
    public GameOverManager gameOverManager;
    public AudioManager audioManager;
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
    }
    private void Start()
    {
        audioManager.PlayBgm(true);
    }
}
