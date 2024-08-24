using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour, IActor
{
    public ActorStatus status;
    protected ProfileDB profileDB;
    public FSMController fsmController { get; private set; }
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
        fsmController = new FSMController(this);
        fsmController.ChangeState(new IdleState());
        detectActor = GetComponent<DetectActor>();        
    }
    protected void Update()
    {
        fsmController.FSMUpdate(); 
    }
    public abstract void ReceiveEvent(IEvent ievent);
    public abstract void TakeDamage(int damage);
    public abstract void DieActor();    
}
