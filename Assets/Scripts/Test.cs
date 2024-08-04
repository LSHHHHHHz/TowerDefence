using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    protected List<Tower> detectedTowers;
    public void DetectActorsInRange(Vector3 center, float radius)
    {
        Collider[] detectedColliders = Physics.OverlapSphere(center, radius);

        HashSet<Tower> newDetectedActors = new HashSet<Tower>();

        foreach (var collider in detectedColliders)
        {
            Tower actor = collider.GetComponent<Tower>();
            if (actor != null)
            {
                newDetectedActors.Add(actor);
            }
        }

        foreach (var actor in newDetectedActors)
        {
            if (!detectedTowers.Contains(actor))
            {
                detectedTowers.Add(actor);
            }
        }

        List<Tower> actorsToRemove = new List<Tower>();
        foreach (var actor in detectedTowers)
        {
            if (!newDetectedActors.Contains(actor))
            {
                actorsToRemove.Add(actor);
            }
        }
        foreach (var actor in actorsToRemove)
        {
            detectedTowers.Remove(actor);
        }
    }
}
public class Test2 : MonoBehaviour
{
    bool isOnTriggerEnter = false;
    Actor actor;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Monster") && !isOnTriggerEnter)
        {
            Debug.Log("적 감지 1개만 할 것");
            isOnTriggerEnter = true;
            actor = other.GetComponent<Actor>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isOnTriggerEnter = false;
        actor = null;
    }
}