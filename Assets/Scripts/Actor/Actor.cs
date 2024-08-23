using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour, IActor
{
    protected ActorStatusDB actorStatusDB;
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
        actorStatusDB = GameManager.instance.gameEntityData.GetActorStatusDB(actorId);
        profileDB = GameManager.instance.gameEntityData.GetProfileData(actorId);
        fsmController = new FSMController(this);
        fsmController.ChangeState(new IdleState());
        detectActor = GetComponent<DetectActor>();
        detectActor.Initialized(actoryType);
    }
    protected void Update()
    {
        fsmController.FSMUpdate(); 
    }
    public abstract void ReceiveEvent(IEvent ievent);
    public abstract void TakeDamage(int damage);
    public abstract void DieActor();    
}
