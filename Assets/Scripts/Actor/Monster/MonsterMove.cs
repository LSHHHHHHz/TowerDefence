using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    Monster monster;
    [SerializeField] GameObject warePoint;
    List<Vector3> monsterMovePos = new List<Vector3>();
    Vector3 targetPos;
    int warePointIndex = 0;
    Quaternion targetRot;
    private void Awake()
    {
        monster = GetComponent<Monster>();
        SetMovePos();
    }
    private void OnEnable()
    {
        warePointIndex = 0;
        targetPos = monsterMovePos[warePointIndex];
        transform.position = targetPos;
    }
    private void Update()
    {
        if (targetPos != null)
        {
            MoveMonster();
        }
    }
    void SetMovePos()
    {
        for (int i = 0; i < warePoint.transform.childCount; i++)
        {
            monsterMovePos.Add(warePoint.transform.GetChild(i).transform.position); 
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
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * monster.monsterAttributes.moveSpeed);
        Vector3 dir = (targetPos - transform.position).normalized;
        dir.y = 0;
        targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * monster.monsterAttributes.rotationSpeed);
    }
}
