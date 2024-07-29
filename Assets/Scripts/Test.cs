using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, IActor
{
    public GameObject test;

    public void ReceiveEvent(IEvent iEvent)
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            SendDamageEvent damage = new SendDamageEvent(this, 10);
            TestActor actor = new TestActor();
            actor.ReceiveEvent(damage);
        }
    }
}
