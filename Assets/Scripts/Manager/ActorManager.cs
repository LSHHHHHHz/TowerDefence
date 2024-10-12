using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager2
{
    private static ActorManager2 instance;
    public static ActorManager2 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ActorManager2();
            }
            return instance;
        }
    }
    List<Actor> actors = new List<Actor>();
    public IEnumerable<T> EnumerateActors<T>() where T : class
    {
        foreach(var actor in actors)
        {
            if (actor is T)
            {
                yield return actor as T;
            }
        }
    }
}

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
    List<T> actors = new List<T>();

    public void RegisterActor(T actor)
    {
        actors.Add(actor);
    }
    public void UnregisterActor(T actor)
    {
        if (actors.Contains(actor)) 
        {
            actors.Remove(actor);
        }
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
    public IReadOnlyList<T> GetActors()
    {
        return actors;
    }
}
