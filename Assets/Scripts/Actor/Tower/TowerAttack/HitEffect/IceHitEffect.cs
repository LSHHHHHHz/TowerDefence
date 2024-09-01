using System;
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
    Coroutine activeObjCoroutine;

    event Action<int> clearData;
    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    private void OnEnable()
    {
        elapsedTime = 0;
        currentRadius = startRadius;
        capsuleCollider.radius = startRadius;
        StartCoroutine(OnEnableObject(activeObjTime));
    }
    private void OnDisable()
    {
        if (activeObjCoroutine != null)
        {
            StopCoroutine(activeObjCoroutine);
        }
        clearData?.Invoke(effectStatusAmount);
        clearData = null;
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
    public void PlayImpactEffect()
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

    // 디버프 지속시간 
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            SendSlowDebuffEvent slow = new SendSlowDebuffEvent(effectStatusAmount);
            IActor actor = other.GetComponent<IActor>();
            actor.ReceiveEvent(slow); //수정필요

            if(actor is Monster monster)
            if (monster != null)
            {
                    monster.ReceiveEvent(slow); 
                
                clearData -= monster.TakeOutSlowDebuff;
                clearData += monster.TakeOutSlowDebuff;
            }
        }
    }
}
