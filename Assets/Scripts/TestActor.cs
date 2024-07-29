using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActor : MonoBehaviour, IActor
{
    public GameObject test;

    public void ReceiveEvent(IEvent iEvent)
    {
        if (iEvent is SendDamageEvent damageEvent)
        {
            TakeDamage(damageEvent.damage);
        }
    }
    public void TakeDamage(int  damage)
    {
        Debug.Log(damage);
    }
}
