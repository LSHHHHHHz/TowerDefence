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
        meshRenderer.material.color = originColor;
        effect = Instantiate(effectPrefab, transform);
    }
    private void OnEnable()
    {
        GameManager.instance.towerGroundEventManager.onMouseEnter += ChangeGroundColorEnterMouse;
        GameManager.instance.towerGroundEventManager.onMouseExit += ChangeGroundColorExitMouse;
    }
    private void OnDisable()
    {
        GameManager.instance.towerGroundEventManager.onMouseEnter -= ChangeGroundColorEnterMouse;
        GameManager.instance.towerGroundEventManager.onMouseExit -= ChangeGroundColorExitMouse;
    }
    public void ChangeGroundColorEnterMouse(TowerGround tower)
    {
        if (tower != null)
        {
            tower.GetComponent<TowerGroundEffect>().GetComponent<MeshRenderer>().material.color = new Color(255, 222, 13);
        }
        //if (meshRenderer != null)
        //{
        //    meshRenderer.material.color = new Color(255, 222, 13);
        //}
    }
    public void ChangeGroundColorExitMouse(TowerGround tower)
    {
        if (tower != null)
        {
            tower.GetComponent<TowerGroundEffect>().GetComponent<MeshRenderer>().material.color = Color.black;
        }
        //if (meshRenderer != null)
        //{
        //    meshRenderer.material.color = Color.black;
        //}
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
