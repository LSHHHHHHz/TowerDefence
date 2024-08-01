using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectActor : MonoBehaviour
{
    bool isOntriggerEnter = false;
    string findActorTag;
    public Actor targetActor;
    public Vector3 actorPosition;
    private void Update()
    {
        if (targetActor != null)
        {
            actorPosition = targetActor.transform.position;
        }
    }
    public void Initialized(ActorType type)
    {
        switch (type)
        {
            case ActorType.NormarMonster:
                findActorTag = "Tower";
                break;
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
            targetActor = other.GetComponent<Actor>();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        isOntriggerEnter = false;
        targetActor = null;
    }
}
