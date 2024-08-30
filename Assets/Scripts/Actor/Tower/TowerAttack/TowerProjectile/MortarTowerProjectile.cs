using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MortarTowerProjectile : BaseProjectile
{
    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.1f) //������ �ߴµ� ���Ϳ� ���� ���� ���� �ֱ� ������ �Ÿ������� ����ü�� ��Ȱ��ȭ
        {
            gameObject.SetActive(false);
        }
    }
    public override void MoveTarget(Vector3 targetPos)
    {
        Debug.Log("�Ѿ� �̵� ��");
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
        Debug.Log("Ÿ�� ��ġ�� ����");
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
