using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MortarTowerBullet : BaseBullet
{
    [SerializeField] GameObject[] effects;
    public override void MoveTarget(Vector3 dir, Vector3 targetPos)
    {
        Debug.Log("총알 이동 중");
        StartCoroutine(MoveBullet(targetPos));
    }
    IEnumerator MoveBullet(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.01f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("타겟 위치에 도달");
    }
    public void ActivateEffect()
    {
        for (int i = 0; i < effects.Length; i++)
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
