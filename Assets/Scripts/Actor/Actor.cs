using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour, IActor
{
    protected ActorStatus status;
    protected ActorStats stats;
    protected ActorProfileData profileData;
    public DetectActor detectActor { get; private set; }
    public string actorId;
    public ActorType actoryType;

    protected virtual void Awake()
    {
        detectActor = GetComponent<DetectActor>();
        detectActor.Initialized(actoryType);
    }
    public abstract void ReceiveEvent(IEvent ievent);
    public abstract void TakeDamage(int damage);
    public abstract void DieActor();    
}
