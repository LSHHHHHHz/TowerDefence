using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHitEffect : MonoBehaviour
{
    [SerializeField] GameObject hitEffectPrefab;

    public void InitializePos(Vector3 pos)
    {
        transform.position = pos;
    }
    public abstract void PlayImpantEffect(int amount);
}
