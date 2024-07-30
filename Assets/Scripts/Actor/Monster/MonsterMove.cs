using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public string monsterStartColor;
    List<Vector3> monsterMovePos;
    int warePointIndex = 0;
    Vector3 targetPos;
    Quaternion targetRot;
    float rotateSpeed = 10;
    float moveSpeed = 5;
    GameData gameData;
    private void Awake()
    {
        gameData = GameData.instance;
    }
    private void OnEnable()
    {
        warePointIndex = 0;
        if (monsterStartColor != "")
        {
            monsterMovePos = gameData.monsterWarePointData.GetWarePointPos(monsterStartColor);
            targetPos = monsterMovePos[warePointIndex];
            transform.position = targetPos;
        }
    }
    private void Update()
    {
        if (targetPos != null)
        {
            MoveMonster();
        }
    }
    void MoveMonster()
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.5f)
        {
            warePointIndex++;
            if (warePointIndex < monsterMovePos.Count)
            {
                targetPos = monsterMovePos[warePointIndex];
            }
            else
            {
                gameObject.SetActive(false);
                return;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);

        Vector3 dir = (targetPos - transform.position).normalized;
        dir.y = 0;
        targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotateSpeed);
    }
}
