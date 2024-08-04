using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MouseSensor : MonoBehaviour
{
    public TowerGround towerGround;

    private void Update()
    {
        ScreenToRayUseMouse();
    }
    void ScreenToRayUseMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            bool isFindGround = false;
            TowerGround ground = hit.collider.GetComponent<TowerGround>();
            if (ground != null)
            {
                Debug.Log("TowerGround »Æ¿Œ");
                towerGround = ground;
                isFindGround = true;
                GameManager.instance.towerGroundEventManager.MouseEnter(towerGround);
                break; 
            }
            if(!isFindGround)
            {
                GameManager.instance.towerGroundEventManager.MouseExit(towerGround);
                towerGround = null;
            }
        }
    }
}