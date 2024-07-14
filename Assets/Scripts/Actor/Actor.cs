using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour, IEvent,IActor
{
    protected ActorStatus status;
    protected ActorStats stats;
    public void ReceiveEvent(IEvent ievent)
    {
        throw new System.NotImplementedException();
    }

    public void SendEvent(IActor actor)
    {
        throw new System.NotImplementedException();
    }
    public abstract void DieActor();
}
