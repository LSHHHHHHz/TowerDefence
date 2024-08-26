using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Actor : MonoBehaviour, IActor
{
    public ActorStatus status;
    protected ProfileDB profileDB;
    public Animator anim;
    public DetectActor detectActor { get; private set; }
    public string actorId;
    public ActorType actoryType;

    protected virtual void Awake()
    {
        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }       
        profileDB = GameManager.instance.gameEntityData.GetProfileDB(actorId);        
        detectActor = GetComponent<DetectActor>();        
    }    
    public abstract void ReceiveEvent(IEvent ievent);
    public abstract void TakeDamage(int damage);
    public abstract void DieActor();    
}
