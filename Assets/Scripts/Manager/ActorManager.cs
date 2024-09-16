using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager<T> where T : Actor
{
    int registerCount = 0;
    int unregistercount = 0;
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
    public List<T> actors = new List<T>();

    public void RegisterActor(T actor)
    {
        registerCount++;

        actors.Add(actor);
    }
    public void UnregisterActor(T actor)
    {
        unregistercount++;

        actors.Remove(actor);
    }
    public void ClearAllActor()
    {
        foreach(var actor in actors)
        {
            if(actor.gameObject.activeSelf)
            {
                actor.gameObject.SetActive(false);
            }
        }
    }
    public List<T> GetActors()
    {
        return new List<T>(actors);
    }
}
