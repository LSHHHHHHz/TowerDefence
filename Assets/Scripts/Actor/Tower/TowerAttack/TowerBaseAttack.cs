using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBaseAttack : MonoBehaviour
{
    [SerializeField] string bulletPrefabPath;
    public void FireBullet(Vector3 dir, Vector3 targetPos)
    {
        BaseBullet bullet = PoolManager.instance.GetObjectFromPool(bulletPrefabPath).GetComponent<BaseBullet>();
        if (bullet != null)
        {
            bullet.MoveTarget(dir, targetPos);
        }
    }
}