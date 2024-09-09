using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Actor : MonoBehaviour, IActor
{
    protected Player player;
    public ActorStatus status;
    public ProfileDB profileDB { get; private set; }
    public Animator anim;
    public string actorId;
    public ActorType actoryType;

    protected virtual void Awake()
    {
        player = GameManager.instance.player;
        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }       
        profileDB = GameManager.instance.gameEntityData.GetProfileDB(actorId);        
    }    
    public abstract void ReceiveEvent(IEvent ievent);
    public abstract void TakeDamage(int damage);        
}
