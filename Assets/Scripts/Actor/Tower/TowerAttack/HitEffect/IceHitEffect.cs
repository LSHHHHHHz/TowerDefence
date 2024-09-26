using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceHitEffect : BaseHitEffect
{
    protected override void Awake()
    {
        base.Awake();
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
