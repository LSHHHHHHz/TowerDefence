using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class BarbarianTowerProjectile : BaseProjectile
{
    Vector3 originPos;
    Quaternion originRot; 
    float projectileRotSpeed = 3000f;
    Transform originalParent;
    private void Awake()
    {
        originPos = transform.localPosition;
        originRot = Quaternion.Euler(0, 310, 40);
        Debug.Log(originRot.eulerAngles);
        projectileMoveSpeed = 8;
        originalParent = transform.parent;
    }
    public override void MoveTarget(Vector3 targetPos, IActor target)
    {        
        if (target is Monster monster)
        {
            targetMonster = monster;
        }
        transform.SetParent(null);
        StopAllCoroutines();
        StartCoroutine(MoveProjectile());
    }
    IEnumerator MoveProjectile()
    {
        while (targetMonster != null && Vector3.Distance(transform.position, targetMonster.transform.position + new Vector3(0,1,0)) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetMonster.transform.position + new Vector3(0,1,0), projectileMoveSpeed * Time.deltaTime);
            transform.Rotate(projectileRotSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }
        SendDamageEvent damage = new SendDamageEvent(towerAttackmount);
        damage.ExcuteEvent(targetMonster);
        transform.SetParent(originalParent);
        transform.localPosition = originPos;
        transform.localRotation = originRot;
    }
}
