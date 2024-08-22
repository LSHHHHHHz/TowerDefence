using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SelectFieldTower : MonoBehaviour
{
    Tower selectTower;
    void SelectTower() //���콺 Ŭ�� �� �׶��� ���� �ִ� Ÿ�� �˻�
    {
        if (Input.GetMouseButton(0) && selectTower != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach(var hit in hits)
            {
                if(hit.collider.CompareTag("Ground"))
                {
                    TowerGround towerGround = hit.collider.GetComponent<TowerGround>();
                    if (towerGround != null && towerGround.currentTower != null)
                    {
                        selectTower = towerGround.currentTower;
                        break;
                    }
                }
            }
        }
    }
    void RestoreSelectTower()
    {

    }
}