using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BaseHitEffect : MonoBehaviour
{
    protected Vector3 originPos;
    protected float elapsedTime = 0;
    protected float intervalTime = 0.05f;
    [SerializeField] protected int combatEffectAmount;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float activeObjTime;
    protected HashSet<Monster> damagedMonsters;
    protected IReadOnlyList<Monster> monsters;
    protected ActorManager<Monster> monsterManager;
    protected virtual void Awake()
    {
        originPos = transform.position;
        monsterManager = ActorManager<Monster>.instnace;
        damagedMonsters = new HashSet<Monster>();
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= intervalTime)
        {
            DetectAndApplyEffect();
            elapsedTime = 0;
        }
    }
    public void Initialize(Actor actor, int amount)
    {
        transform.position = new Vector3(actor.transform.position.x, originPos.y, actor.transform.position.z);
        combatEffectAmount = amount;
    }
    protected void OnEnable()
    {
        damagedMonsters.Clear();
        StartCoroutine(OnActiveEffect());
    }
    protected virtual void OnDisable()
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
        monsters = monsterManager.GetActors();
        if(monsters == null)
        {
            return;
        }
        foreach (var monster in monsters)
        {
            float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                                              new Vector3(monster.transform.position.x, 0, monster.transform.position.z));
            if (distance <= attackRange && !damagedMonsters.Contains(monster))
            {
                ApplyEffect(monster);
                damagedMonsters.Add(monster);
            }
            if (distance > attackRange)
            {
                monster.TakeOutSlowDebuff(combatEffectAmount);
                damagedMonsters.Remove(monster);
            }
        }
    }
    protected abstract void ApplyEffect(Monster monster);
}
