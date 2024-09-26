using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
public class FireHitEffect : BaseHitEffect
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