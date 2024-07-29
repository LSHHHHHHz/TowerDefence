using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour, IActor
{
    protected ActorStatus status;
    protected ActorStats stats;   
    public void ReceiveEvent(IEvent ievent)
    {
        ievent.ExcuteEvent(this);
    }
    public abstract void DieActor();
}
