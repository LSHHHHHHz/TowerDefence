using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MortarTowerProjectile : BaseProjectile
{
    [SerializeField] GameObject[] effects;
    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            gameObject.SetActive(false);
        }
    }
    public override void MoveTarget(Vector3 targetPos)
    {
        Debug.Log("총알 이동 중");
        Vector3 adjustPos = new Vector3(targetPos.x, targetPos.y + 2, targetPos.z);
        this.targetPos = adjustPos;
        StartCoroutine(MoveBullet(adjustPos));
    }
    IEnumerator MoveBullet(Vector3 targetPos)
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
            SendDamageEvent damage = new SendDamageEvent(attackDamage);
            IActor actor = other.GetComponent<IActor>();
            damage.ExcuteEvent(actor);
            gameObject.SetActive(false);
        }
    }
}
