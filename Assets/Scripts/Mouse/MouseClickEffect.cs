using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickEffect : MonoBehaviour
{
    MouseInteraction mouseInteraction;

    [SerializeField] RectTransform rt;
    [SerializeField] GameObject effectPrefab;
    private void Awake()
    {
        mouseInteraction = GetComponent<MouseInteraction>();
    }
    private void OnEnable()
    {
        mouseInteraction.onActiveMouseEffect += ActiveMouseClickEffect;
    }
    private void OnDisable()
    {
        mouseInteraction.onActiveMouseEffect -= ActiveMouseClickEffect;
    }
    public void ActiveMouseClickEffect()
    {
        Vector2 localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Input.mousePosition, null, out localPoint))
        {
            GameObject effect =PoolManager.instance.GetObjectFromPool("Prefabs/UI/MouseEffect");
            RectTransform effectRectTransform = effect.GetComponent<RectTransform>();
            effectRectTransform.SetParent(rt);
            effectRectTransform.anchoredPosition = localPoint;
            effectRectTransform.localScale = Vector3.one; 
        }
    }
}