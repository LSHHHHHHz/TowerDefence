using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITowerBasicAttack 
{
    void FireBullet();
    void InitializedBullet(Vector3 dir, Vector3 targetPos);
}