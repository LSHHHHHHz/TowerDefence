using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorDetectorRangeImage : MonoBehaviour
{
    [SerializeField] RectTransform imageRectTransform;
    private void Start()
    {
        imageRectTransform.gameObject.SetActive(false);
    }
    public void ShowActorDetectRange(float range)
    {
        imageRectTransform.gameObject.SetActive(true);
        imageRectTransform.localScale = new Vector3(range, range, range);
    }
    public void CloseActorDetectRange()
    {
        imageRectTransform.gameObject.SetActive(false);
    }
}