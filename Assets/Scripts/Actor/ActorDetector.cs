using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorDetector<T> : BaseDetector where T : Actor
{
    ActorManager<T> actorManager;
    public T targetActor;
    public List<T> detectedActors = new List<T>();
    protected void Awake()
    {
        actorManager = ActorManager<T>.instnace;
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= intervalTime)
        {
            UpdateDetection();
            elapsedTime = 0;
        }
    }
    protected override void UpdateDetection()
    {
        detectedActors.Clear();
        IReadOnlyList<T> actors = actorManager.GetActors();

        if (actors != null)
        {
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
        if (targetActor != null && detectedActors.Contains(targetActor))
        {
            return;
        }
        else
        {
            FindTargetActor();
        }
    }
    protected void FindTargetActor()
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