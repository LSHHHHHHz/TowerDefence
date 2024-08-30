using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class AttributeTowerProjectile : BaseProjectile
{
    [SerializeField] GameObject hitEffectPrefab;
    BaseHitEffect hitEffect;
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
        if (other.CompareTag("Monster"))
        {
            if (hitEffect == null)
            {
                hitEffect = Instantiate(hitEffectPrefab, PoolManager.instance.transform).GetComponent<BaseHitEffect>();
            }
            else
            {
                hitEffect.gameObject.SetActive(true);
            }
            hitEffect.Initialize(transform.position, towerAttackmount);
            gameObject.SetActive(false);
        }
    }
}
