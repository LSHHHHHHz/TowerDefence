using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager<T> where T : Actor
{
    private static ActorManager<T> _instnace;
    public static ActorManager<T> instnace
    {
        get
        {
            if (_instnace == null)
            {
                _instnace = new ActorManager<T>();
            }
            return _instnace;
        }
    }
    public List<T> actors;

    public void RegisterActor(T actor)
    {
        actors.Add(actor);
    }
    public void UnregisterActor(T actor)
    {
        actors.Remove(actor);
    }
    public List<T> GetActors()
    {
        return new List<T>(actors);
    }
}
