using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class SelectFieldTower : MonoBehaviour
{
    Tower selectTower;
    private void Update()
    {
        SelectTower();
    }
    void SelectTower() //마우스 클릭 시 그라운드 위에 있는 타워 검색
    {
        if (Input.GetMouseButton(0))
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