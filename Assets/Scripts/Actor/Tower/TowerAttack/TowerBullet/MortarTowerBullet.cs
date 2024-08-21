using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MortarTowerBullet : BaseBullet
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
        Debug.Log("�Ѿ� �̵� ��");
        Vector3 adjustPos = new Vector3(targetPos.x, targetPos.y + 2, targetPos.z);
        this.targetPos = adjustPos;
        StartCoroutine(MoveBullet(adjustPos));
    }
    IEnumerator MoveBullet(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.01f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("Ÿ�� ��ġ�� ����");
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            SendDamageEvent damage = new SendDamageEvent(bulletDamage);
            IActor actor = other.GetComponent<IActor>();
            damage.ExcuteEvent(actor);
            gameObject.SetActive(false);
        }
    }
}
