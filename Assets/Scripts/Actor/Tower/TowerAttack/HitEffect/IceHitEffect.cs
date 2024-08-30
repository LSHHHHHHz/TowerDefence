using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceHitEffect : BaseHitEffect
{
    CapsuleCollider capsuleCollider;

    float currentRadius = 1f;

    float startRadius = 1f;
    float maxRadius = 20;
    float duration = 1f;

    float elapsedTime = 0f;
    float activeObjTime = 2.5f;
    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    private void OnEnable()
    {
        StartCoroutine(OnEnableObject(activeObjTime));
    }
    private void OnDisable()
    {
        elapsedTime = 0;
        currentRadius = startRadius;
        capsuleCollider.radius = startRadius;
    }
    private void Update()
    {
        PlayImpactEffect();
    }
    IEnumerator OnEnableObject(float activeTime)
    {
        yield return new WaitForSeconds(activeTime);
        gameObject.SetActive(false);
    }
    public override void PlayImpactEffect()
    {
        elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp(elapsedTime / duration, 0f, duration);
        currentRadius = Mathf.Lerp(startRadius, maxRadius, t);

        if (t >= 1f)
        {
            elapsedTime = duration;
        }
        capsuleCollider.radius = currentRadius;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            SendSlowDebuffEvent slow = new SendSlowDebuffEvent(effectStatusAmount);
            IActor actor = other.GetComponent<IActor>();
            if (actor != null)
            {
                actor.ReceiveEvent(slow);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster actor = other.GetComponent<Monster>();
            if (actor != null)
            {
                actor.TakeOutSlowDebuff(effectStatusAmount);
            }
        }
    }
}
