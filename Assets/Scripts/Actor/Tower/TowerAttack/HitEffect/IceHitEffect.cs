using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceHitEffect : BaseHitEffect
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        monsters = monsterManager.GetActors();
        foreach (var monster in monsters)
        {
            monster.TakeOutSlowDebuff(combatEffectAmount);
            damagedMonsters.Remove(monster);
        }
    }
    protected override void ApplyEffect(Monster monster)
    {
        SendSlowDebuffEvent damage = new SendSlowDebuffEvent(combatEffectAmount);
        IActor actor = monster.GetComponent<IActor>();
        if (actor != null)
        {
            actor.ReceiveEvent(damage);
        }
    }
}
