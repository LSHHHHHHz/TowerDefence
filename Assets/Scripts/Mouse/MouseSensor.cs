using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MouseSensor : MonoBehaviour
{
    public TowerGround groundColor;
    public TowerGround dragTowerGround;
    public TowerGround dropTowerGround;
    public Tower dragTower;
    public Tower dropTower;
    HaveTowerData haveTower;

    private void Update()
    {
        SelectTower();
        ScreenToRayUseMouse();
        MoveTower();
    }
    void MoveTower()
    {
        if (dragTower != null) //타워가 있다면 해당 타워 마우스랑 같이 이동
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                Vector3 adjustPoint = new Vector3(point.x, 1, point.z);
                dragTower.transform.position = adjustPoint;
            }
        }
        if (Input.GetMouseButtonUp(0) && dragTower != null) //마우스를 놓았을 때 초기화
        {
            //새로운 곳에 배치가 되지 않으면 기존 위치로 다시 이동
            dragTower = null;
            haveTower = HaveTowerData.None;
        }
    }
    void SelectTower()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits)
            {
                TowerGround ground = hit.collider.gameObject.GetComponent<TowerGround>();
                if(ground != null)
                {
                    dragTower = ground.currentTower;
                    ground.currentTower = null;
                }
            }
        }
    }
    void ScreenToRayUseMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        bool isFindGround = false;

        foreach (RaycastHit hit in hits)
        {
            TowerGround towerGround = hit.collider.GetComponent<TowerGround>();

            if (towerGround != null && !towerGround.ISHasTower()) //타워가 배치되어 있지 않을 때
            {
                GameManager.instance.towerGroundEventManager.MouseExit(groundColor);
                groundColor = towerGround;
                isFindGround = true;
                GameManager.instance.towerGroundEventManager.MouseEnter(groundColor);
                break;
            }
            if (towerGround != null && towerGround.ISHasTower())//타워가 배치되어 있을 때
            {
                GameManager.instance.towerGroundEventManager.MouseExit(groundColor);
            }
        }
        if (!isFindGround) //어떠한 것도 검출되어있지 않다면 원상복귀
        {
            GameManager.instance.towerGroundEventManager.MouseExit(groundColor);
            groundColor = null;
        }
    }
}