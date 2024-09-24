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
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            gameObject.SetActive(false);

            hitEffect = PoolManager.instance.GetObjectFromPool(hitEffectPrefabPath).GetComponent<BaseHitEffect>();
            if (hitEffect != null)
            {
                hitEffect.Initialize(targetPos, towerAttackmount);
            }
        }
    }
    public override void MoveTarget(Vector3 targetPos, IActor target)
    {
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
    }
}
