using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHitEffect : BaseHitEffect
{
    CapsuleCollider capsuleCollider;
    float activeColliderTime = 3f;
    float activeObjTime = 4f;
    private Coroutine colliderCoroutine;
    private Coroutine objCoroutine;
    private void Awake()
    {
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
            SendDamageEvent damage = new SendDamageEvent(effectStatusAmount);
            IActor actor = other.GetComponent<IActor>();
            if (actor != null)
            {
                actor.ReceiveEvent(damage);
            }
        }
    }
}
