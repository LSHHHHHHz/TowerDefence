using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BaseHitEffect : MonoBehaviour
{
    protected Vector3 originPos;
    [SerializeField]protected int combatEffectAmount;
    [SerializeField]protected float attackRange;
    [SerializeField]protected float activeObjTime;
    private HashSet<Monster> damagedMonsters;

    protected virtual void Awake()
    {
        originPos = transform.position;
        damagedMonsters = new HashSet<Monster>();
    }
    public void Initialize(Actor actor, int amount)
    {
        transform.position = new Vector3(actor.transform.position.x, originPos.y, actor.transform.position.z);
        combatEffectAmount = amount;
        DetectAndApplyEffect();
    }
    protected void OnEnable()
    {
        damagedMonsters.Clear();
        StartCoroutine(OnActiveEffect());
    }
    protected void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator OnActiveEffect()
    {
        float time = 0;
        while (time < activeObjTime)
        {
            time += Time.deltaTime;            
            yield return null;
        }
        gameObject.SetActive(false);
    }
    protected virtual void DetectAndApplyEffect()
    {
        ActorManager<Monster> monsterManager = ActorManager<Monster>.instnace;
        IReadOnlyList<Monster> monsters = monsterManager.GetActors();

        foreach (var monster in monsters)
        {
            float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                              new Vector3(monster.transform.position.x, 0, monster.transform.position.z));
            if (distance <= attackRange && !damagedMonsters.Contains(monster))
            {
                ApplyEffect(monster);
                damagedMonsters.Add(monster);
            }
        }
    }
    protected abstract void ApplyEffect(Monster monster);
}
