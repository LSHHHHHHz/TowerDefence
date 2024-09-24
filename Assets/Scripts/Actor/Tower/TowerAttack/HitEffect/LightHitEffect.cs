using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHitEffect : BaseHitEffect
{
    public CapsuleCollider capsuleCollider;
    float activeColliderTime = 1f;
    float activeObjTime = 1.2f;
    private Coroutine colliderCoroutine;
    private Coroutine objCoroutine;
    protected override void Awake()
    {
        base.Awake();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    private void OnEnable()
    {
        capsuleCollider.enabled = true;
        colliderCoroutine = StartCoroutine(OnEnableColliderTime(activeColliderTime));
        objCoroutine = StartCoroutine(OnEnableObjTime(activeObjTime));
    }
    private void OnDisable()
    {
        if (colliderCoroutine != null)
        {
            StopCoroutine(colliderCoroutine);
        }
        if (objCoroutine != null)
        {
            StopCoroutine(objCoroutine);
        }
    }
    IEnumerator OnEnableColliderTime(float activeColliderTime)
    {
        yield return new WaitForSeconds(activeColliderTime);
        capsuleCollider.enabled = false;
    }

    IEnumerator OnEnableObjTime(float activeObjTime)
    {
        yield return new WaitForSeconds(activeObjTime);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            SendDamageEvent damage = new SendDamageEvent(combatEffectAmount);
            IActor actor = other.GetComponent<IActor>();
            if (actor != null)
            {
                Debug.LogError("몇번호출되냐이거");
                actor.ReceiveEvent(damage);
            }
        }
    }

    protected override void UpdateDetection()
    {
    }
}
