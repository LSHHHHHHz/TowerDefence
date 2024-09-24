using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianMaterial : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] GameObject body;
    [SerializeField] GameObject axe;

    private void Awake()
    {
        switch (level)
        {
            case 0:
                SetMaterialColor(body, Color.gray);
                SetMaterialColor(axe, Color.gray);
                break;
            case 1: 
                SetMaterialColor(body, Color.red);
                SetMaterialColor(axe, Color.red);
                break;
            case 2:
                SetMaterialColor(body, Color.yellow);
                SetMaterialColor(axe, Color.yellow);
                break;
            default:
                break;
        }
    }
    private void SetMaterialColor(GameObject obj, Color color)
    {
        if (obj != null)
        {
            Renderer objRenderer = obj.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                objRenderer.material = new Material(objRenderer.material);
                objRenderer.material.color = color;
            }
        }
    }
}
