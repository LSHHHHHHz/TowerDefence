using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MortarTowerBullet : BaseBullet
{
    [SerializeField] GameObject[] effects;
    public override void MoveTarget(Vector3 dir, Vector3 targetPos)
    {
        Debug.Log("ÃÑ¾Ë ÀÌµ¿ Áß");
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
