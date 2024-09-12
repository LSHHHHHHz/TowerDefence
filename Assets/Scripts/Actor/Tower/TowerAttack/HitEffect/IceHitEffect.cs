using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceHitEffect : BaseHitEffect
{
    CapsuleCollider capsuleCollider;
    List<Monster> monstersInRange = new List<Monster>(); // 범위 내 몬스터들 추적

    float currentRadius = 1f;
    float startRadius = 1f;
    float maxRadius = 20f;
    float duration = 1f;

    float elapsedTime = 0f;
    float activeObjTime = 2.5f;
    Coroutine activeObjCoroutine;
    protected override void Awake()
    {
        base.Awake();
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
        foreach (Monster monster in monstersInRange)
        {
            if (monster != null)
            {
                monster.TakeOutSlowDebuff(combatEffectAmount);
            }
        }
        monstersInRange.Clear();
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            SendSlowDebuffEvent slow = new SendSlowDebuffEvent(combatEffectAmount);
            IActor actor = other.GetComponent<IActor>();
            actor.ReceiveEvent(slow);

            if (actor is Monster monster)
            {
                monstersInRange.Add(monster); 
                monster.ReceiveEvent(slow);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            IActor actor = other.GetComponent<IActor>();
            if (actor is Monster monster)
            {
                if (monster != null)
                {
                    monster.TakeOutSlowDebuff(combatEffectAmount);
                    monstersInRange.Remove(monster); 
                }
            }
        }
    }
}
