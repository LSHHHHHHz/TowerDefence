using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGroundEffect : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Color originColor = Color.black;
    [SerializeField] GameObject effectPrefab;
    GameObject effect;
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originColor = meshRenderer.material.color;
        meshRenderer.material.color = originColor;
        if (effectPrefab != null)
        {
            effect = Instantiate(effectPrefab, transform);
        }
    }
    public void ChangeGroundColorEnterMouse()
    {
        meshRenderer.material.color = new Color(255, 222, 13);
    }
    public void ChangeGroundColorExitMouse()
    {
        meshRenderer.material.color = originColor;
    }
    public void ChangeGroundColorInTower()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
            effect.SetActive(true);
        }
    }
    public void ChangeGroundColorOutTower()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = true;
            meshRenderer.material.color = originColor;
            effect.SetActive(false);
        }
    }
}
