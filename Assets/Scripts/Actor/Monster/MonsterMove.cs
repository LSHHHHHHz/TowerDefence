using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    List<List<Vector3>> monsterMovePos;
    List<Vector3> startMonsterMovePos;
    int warePointIndex = 0;
    Vector3 targetPos;
    Quaternion targetRot;
    public float rotateSpeed = 10;
    public float moveSpeed = 5;
    GameData gameData;
    private void Awake()
    {
        gameData = GameData.instance;
        monsterMovePos = new List<List<Vector3>>();
        monsterMovePos.Add(new List<Vector3>(gameData.monsterData.monsterWarePointDatas.GetWarePointPos("Red")));
        monsterMovePos.Add(new List<Vector3>(gameData.monsterData.monsterWarePointDatas.GetWarePointPos("Blue")));
    }
    private void Start()
    {
        targetPos = startMonsterMovePos[warePointIndex];
    }
    private void Update()
    {
        MoveMonster();
    }
    void MoveMonster()
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.5f)
        {
            warePointIndex++;
            targetPos = startMonsterMovePos[warePointIndex];
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);

        Vector3 dir = (targetPos - transform.position).normalized;
        dir.y = 0;
        targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotateSpeed);
    }
    public void StartMonster(string color)
    {
        switch (color)
        {
            case "Red":
                startMonsterMovePos = monsterMovePos[0];
                break;
            case "Blue":
                startMonsterMovePos = monsterMovePos[1];
                break;

        }
    }
}
