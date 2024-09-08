using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetector : MonoBehaviour
{
    public Actor targetActor;
    public List<Actor> detectedActors;

    private void Update()
    {
        detectedActors.Clear();

        if (targetActor != null)
        {
            // monster 목록을 가져오기
            List<Monster> monsters = null;
            foreach (var monster in monsters)
            {
                // 거리 안에 있는 몬스터
                detectedActors.Add(monster);
            }
        }
    }
}

public class DetectActor : MonoBehaviour
{
    public string TargetTag;

    bool isOntriggerEnter = false;
    string findActorTag;
    public Actor targetActor;
    public Vector3 targetPosition;

    public Actor detectedActor;

    private void Update()
    {
        if (targetActor != null)
        {
            targetPosition = targetActor.transform.position;
        }
        
    }
    public void Initialized(string targetTag)
    {
        findActorTag = targetTag;
        //switch (type)
        //{
        //    case ActorType.NormarMonster:
        //        findActorTag = "Tower";
        //        break;
        //    case ActorType.BossMonster:
        //        findActorTag = "Tower";
        //        break;
        //    case ActorType.NormarTower:
        //        findActorTag = "Monster";
        //        break;
        //    case ActorType.ChampionTower:
        //        findActorTag = "Monster";
        //        break;
        //}
    }


    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag(findActorTag) && !isOntriggerEnter)
        {
            Actor targetActor = other.GetComponent<Actor>();
            if (targetActor != null)
            {
                detectedActor = targetActor;
            }



            //Debug.Log(other.gameObject.name);
            //isOntriggerEnter = true;
            //IDetected targetActor = other.GetComponent<IDetected>();
            //targetActor
            //if(targetActor is Monster monster)
            //{
            //    monster.onMonsterDeath += HandleMonsterDeath;
            //}
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isOntriggerEnter = false;
        targetActor = null;
        if (targetActor is Monster monster)
        {
            monster.onMonsterDeath -= HandleMonsterDeath;
        }
    }
    void HandleMonsterDeath(Monster monster)
    {
        isOntriggerEnter = false;
        targetActor = null;
        monster.onMonsterDeath -= HandleMonsterDeath;
    }
}
