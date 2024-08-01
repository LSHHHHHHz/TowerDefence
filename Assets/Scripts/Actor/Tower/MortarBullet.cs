using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBullet : MonoBehaviour , IBullet
{
    public GameObject[] effects;
    Vector3 dir;
    Vector3 targetPos;

    //현재 위치와 목표물 위치의 중간위쪽까지 이동 후 중간위치에 도착하면 목표 위치로 이동
    //발사했을때 넘겨온 데이터만으로 총알이 나갈지, 발사후에도 검출안 게임오브젝트의 위치로 유도미사일처럼 할지 고민 중
    //Detect에 움직이고 있는 정보와 처음 검출된 정보는 모두 있음
    public void InitializedBullet(Vector3 dir, Vector3 targetPos)
    {
        this.dir = dir;
        this.targetPos = targetPos;
    }
    public void FireBullet()
    {
        Debug.Log("박격포 같은넘 공격하나?");
    }
    public void ActivateEffect()
    {
        for (int i = 0; i < effects.Length;  i++)
        {
            effects[i].SetActive(true);
        }
    }
    public void DisabledEffect()
    {
        for (int i = 0; i < effects.Length; i++)
        {
            effects[i].SetActive(false);
        }
    }
}
