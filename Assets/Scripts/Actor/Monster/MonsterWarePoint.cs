using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWarePoint : MonoBehaviour
{
    public Transform[] warePointPos;
    int warePointIndex = 0;
    Vector3 targetPos;
    Quaternion targetRot;
    public float rotateSpeed = 10;
    public float moveSpeed = 5;
    private void Start()
    {
        targetPos = warePointPos[warePointIndex].position;
    }
    private void Update()
    {
        if(Vector3.Distance(transform.position,targetPos)<0.5f)
        {
            warePointIndex++;
            targetPos = warePointPos[warePointIndex].position;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);

        Vector3 dir = (targetPos - transform.position).normalized;
        dir.y = 0;
        targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotateSpeed);
    }
}
