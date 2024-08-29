using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    TowerEventHandler towerEventHandler;
    public Tower dragTower;
    public TowerData dropTower;
    private void Awake()
    {
        towerEventHandler = TowerGroundManager.instance.towerEventHandler;
    }
    private void Update()
    {
        MouseButtonDown();
        MouseButtonUP();
        ScreenToRayUseMouse();
        MoveTower();
    }
    void MoveTower()
    {
        if (dragTower != null)
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
    }
    void MouseButtonDown()
    {
        bool isClick = false;
        if (Input.GetMouseButtonDown(0) && towerEventHandler.detectedTowerData == null) // 선택된 타워 데이터가 없는 경우
        {
            isClick = true;
            if (towerEventHandler.detectedTowerGroundData != null && towerEventHandler.detectedTowerGroundData.towerData != null)
            {
                towerEventHandler.detectedTowerData = towerEventHandler.detectedTowerGroundData.towerData;
                towerEventHandler.detectedTowerGroundData.RemoveTower();
            }
            else
            {
                Debug.Log("그라운드에 타워가 없음");
                return;
            }
        }
        if (Input.GetMouseButtonDown(0) && towerEventHandler.detectedTowerData != null && !isClick) // 선택된 타워 데이터가 있는 경우
        {
            if (towerEventHandler.detectedTowerGroundData != null && towerEventHandler.detectedTowerGroundData.towerData != null)
            {
                towerEventHandler.detectedTowerData = towerEventHandler.detectedTowerGroundData.towerData;
                towerEventHandler.detectedTowerGroundData.RemoveTower();
            }
            else
            {
                Debug.Log("그라운드에 타워가 없음");
                return;
            }
        }
    }
    void MouseButtonUP()
    {
        if (Input.GetMouseButtonUp(0))
        {

        }
        if (towerEventHandler.detectedTowerGroundData == null)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0) && towerEventHandler.detectedTowerData != null) //무언가 이동 중이라면
        {
            //현재 그라운드데이터에 타워가 있는 경우
            //같은 좋류, 레벨의 타워라면
            //다른 종류의 타워라면

            //현재 그라운드에티어에 타워가 없는 경우
            if (towerEventHandler.detectedTowerGroundData.towerData.towerID == null)
            {
                towerEventHandler.SetTower(towerEventHandler.detectedTowerGroundData, towerEventHandler.detectedTowerData);
                towerEventHandler.detectedTowerGroundData.towerData = towerEventHandler.detectedTowerData;
                towerEventHandler.detectedTowerData = null;
            }
        }
    }
    void ScreenToRayUseMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        bool isFindGround = false;

        foreach (RaycastHit hit in hits)
        {
            TowerGround towerGround = hit.collider.GetComponent<TowerGround>();

            if (towerGround != null)
            {
                if (towerEventHandler.detectedTowerGroundData != null)
                {
                    towerEventHandler.ExitTowerGround(towerEventHandler.detectedTowerGroundData);
                }
                towerEventHandler.detectedTowerGroundData = towerGround.towerGroundData;

                isFindGround = true;
                towerEventHandler.EnterTowerGround(towerEventHandler.detectedTowerGroundData);
                break;
            }
        }

        if (!isFindGround)
        {
            if (towerEventHandler.detectedTowerGroundData != null)
            {
                towerEventHandler.ExitTowerGround(towerEventHandler.detectedTowerGroundData);
            }
            towerEventHandler.detectedTowerGroundData = null;
        }
    }
}
