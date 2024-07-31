using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectActor : MonoBehaviour
{
    bool isOntriggerEnter = false;
    string findActorTag;
    Actor actor;
    public Vector3 actorPosition { get; private set; }
    private void Update()
    {
        if (actor != null)
        {
            actorPosition = actor.transform.position;
        }
    }
    public void Initialized(ActorType type)
    {
        switch (type)
        {
            case ActorType.BossMonster:
                findActorTag = "Tower";
                break;
            case ActorType.NormarTower:
                findActorTag = "Monster";
                break;
            case ActorType.ChampionTower:
                findActorTag = "Monster";
                break;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(findActorTag) && !isOntriggerEnter)
        {
            Debug.Log(other.gameObject.name);
            isOntriggerEnter = true;
            actor = other.GetComponent<Actor>();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        isOntriggerEnter = false;
        actor = null;
    }
}
