using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MortarTowerProjectile : BaseProjectile
{
    Monster targetMonster;
    private void Update()
    {
        if (target != null && targetMonster != null)
        {
            Vector3 adjustPos = new Vector3(targetMonster.gameObject.transform.position.x, targetMonster.gameObject.transform.position.y, targetMonster.gameObject.transform.position.z);
            targetPos = adjustPos;
        }
        else
        {
            targetMonster = null;
        }
        if (Vector3.Distance(transform.position, targetMonster.transform.position) < 0.1f)
        {
            SendDamageEvent damage = new SendDamageEvent(towerAttackmount);
            if (target != null)
            {
                damage.ExcuteEvent(target);
            }
            gameObject.SetActive(false);
        }
    }
    public override void MoveTarget(Vector3 targetPos, IActor target)
    {
        if(target is Monster monster)
        {
            targetMonster = monster;
        }
        StartCoroutine(MoveProjectile());
    }
    IEnumerator MoveProjectile()
    {
        while (Vector3.Distance(transform.position, targetMonster.transform.position) > 0.01f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, projectileMoveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
