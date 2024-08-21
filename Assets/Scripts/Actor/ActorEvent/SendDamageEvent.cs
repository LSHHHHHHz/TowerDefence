using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SendDamageEvent : IEvent
{
    public int damage;

    public SendDamageEvent( int damage)
    {
        this.damage = damage;
    }
    public void ExcuteEvent(IActor target)
    {
        target.ReceiveEvent(this);
    }
}
