using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorDetector<T> : MonoBehaviour where T : Actor
{
    ActorManager<T> actorManager;
    public T targetActor;
    public List<T> detectedActors = new List<T>();

    float elapsedTime = 0;
    float intervalTime = 0.05f;
    private float _detectionRange = 1; 
    public float detectionRange
    {
        get { return _detectionRange; }
        set { _detectionRange = value; }
    }
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
        IReadOnlyList<T> actors = actorManager.GetActors();
        foreach (var actor in actors)
        {
            Vector3 direction = transform.position - actor.transform.position;
            direction.y = 0;  

            float distance = direction.magnitude; 
            if (distance < detectionRange)
            {
                detectedActors.Add(actor);
            }
        }
    }
    void FindTargetActor()
    {
        if (detectedActors.Count > 0)
        {
            int num = Random.Range(0, detectedActors.Count);
            targetActor = detectedActors[num];
        }
        else
        {
            targetActor = null;
        }
    }
}