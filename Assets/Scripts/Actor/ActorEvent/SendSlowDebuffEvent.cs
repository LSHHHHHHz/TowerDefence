using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SendSlowDebuffEvent : IEvent
{
    public int slowDebuffAmount;

    public SendSlowDebuffEvent( int amount)
    {
        this.slowDebuffAmount = amount;
    }
    public void ExcuteEvent(IActor target)
    {
        target.ReceiveEvent(this);
    }
}
