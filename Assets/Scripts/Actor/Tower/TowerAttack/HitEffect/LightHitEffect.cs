using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHitEffect : BaseHitEffect
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void ApplyEffect(Monster monster)
    {
        SendDamageEvent damage = new SendDamageEvent(combatEffectAmount);
        IActor actor = monster.GetComponent<IActor>();
        if (actor != null)
        {
            actor.ReceiveEvent(damage);
        }
    }
}
