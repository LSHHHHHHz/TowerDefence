using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGroundEffect : MonoBehaviour 
{
    MeshRenderer meshRenderer;
    Color originColor = Color.black;
    [SerializeField] GameObject effectPrefab;
    GameObject effect;
    TowerGround towerGround;
    void Awake()
    {
        towerGround = GetComponent<TowerGround>();
        meshRenderer = GetComponent<MeshRenderer>();
        originColor = meshRenderer.material.color;
        meshRenderer.material.color = originColor;
        if (effectPrefab != null)
        {
            effect = Instantiate(effectPrefab, transform);
        }
    }
    private void OnEnable()
    {
        towerGround.towerGroundData.enterTowerGround += ChangeGroundColorEnterMouse;
        towerGround.towerGroundData.exitTowerGround += ChangeGroundColorExitMouse;
    }
    private void OnDisable()
    {
        towerGround.towerGroundData.enterTowerGround -= ChangeGroundColorEnterMouse;
        towerGround.towerGroundData.exitTowerGround -= ChangeGroundColorExitMouse;
    }
    public void ChangeGroundColorEnterMouse(TowerGroundData data)
    {
        if (data != null && data == towerGround.towerGroundData)
        {
            meshRenderer.material.color = new Color(255, 222, 13);
        }
    }
    public void ChangeGroundColorExitMouse(TowerGroundData data)
    {
        if (data != null)
        {
            meshRenderer.material.color = originColor;
        }
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
