using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActor 
{
    void ReceiveEvent(IEvent ievent);
}
public interface IEvent
{
    void SendEvent(IActor actor);
}
