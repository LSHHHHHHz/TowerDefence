using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class AttributeTowerProjectile : BaseProjectile
{
    [SerializeField] string hitEffectPrefabPath;
    BaseHitEffect hitEffect;
    private void Update()
    {
        if (Vector3.Distance(transform.position, targetMonster.transform.position + new Vector3(0,1,0)) < 0.1f)
        {
            gameObject.SetActive(false);

            hitEffect = PoolManager.instance.GetObjectFromPool(hitEffectPrefabPath).GetComponent<BaseHitEffect>();
            if (hitEffect != null)
            {
                hitEffect.transform.position = new Vector3(targetMonster.transform.position.x, hitEffect.transform.position.y, targetMonster.transform.position.z);
                hitEffect.Initialize(targetMonster, towerAttackmount);
            }
        }
    }
    public override void MoveTarget(Vector3 targetPos, IActor target)
    {
        if (target is Monster monster)
        {
            targetMonster = monster;
        }
        StartCoroutine(MoveProjectile());
    }
    IEnumerator MoveProjectile()
    {
        while (targetMonster != null && Vector3.Distance(transform.position, targetMonster.transform.position + new Vector3(0, 1, 0)) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetMonster.transform.position + new Vector3(0, 1, 0), projectileMoveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
