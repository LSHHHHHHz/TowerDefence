using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorDetector<T> : MonoBehaviour where T : Actor
{
    ActorManager<T> actorManager;
    public T targetActor;
    public List<T> detectedActors = new List<T>();

    float elapsedTime = 0;
    [SerializeField] float intervalTime = 0.05f;
    public float detectionRange = 5;
    private void Awake()
    {
        actorManager = ActorManager<T>.instnace;
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= intervalTime)
        {
            UpdateDetectActors();
            FindTargetActor();
            elapsedTime = 0;
        }
    }
    void UpdateDetectActors()
    {
        detectedActors.Clear();
        // monster 목록을 가져오기
        List<T> actors = actorManager.GetActors();
        foreach (var actor in actors)
        {
            // 거리 안에 있는 몬스터
            if (Vector3.Distance(transform.position, actor.transform.position) < detectionRange)
            {
                detectedActors.Add(actor);
            }
        }
    }
    void FindTargetActor()
    {
        if (detectedActors.Count > 0)
        {
            int num = UnityEngine.Random.Range(0, detectedActors.Count);
            targetActor = detectedActors[num];
        }
        else
        {
            targetActor = null;
        }
    }
}