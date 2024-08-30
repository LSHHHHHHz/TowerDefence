using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MortarTowerProjectile : BaseProjectile
{
    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.1f) //공격을 했는데 몬스터에 맞지 않을 수도 있기 때문에 거리상으로 투사체를 비활성화
        {
            gameObject.SetActive(false);
        }
    }
    public override void MoveTarget(Vector3 targetPos)
    {
        Debug.Log("총알 이동 중");
        Vector3 adjustPos = new Vector3(targetPos.x, targetPos.y + 2, targetPos.z);
        this.targetPos = adjustPos;
        StartCoroutine(MoveProjectile(adjustPos));
    }
    IEnumerator MoveProjectile(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.01f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, projectileMoveSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("타겟 위치에 도달");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            SendDamageEvent damage = new SendDamageEvent(towerAttackmount);
            IActor actor = other.GetComponent<IActor>();
            if (actor != null)
            {
                damage.ExcuteEvent(actor);
            }
            gameObject.SetActive(false);
        }
    }
}
