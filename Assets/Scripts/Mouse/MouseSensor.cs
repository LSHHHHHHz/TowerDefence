using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MouseSensor : MonoBehaviour
{
    public TowerGroundData detectedTowerGroundData;
    public TowerData detectedTowerData;

    public string detectedTowerID;
    public Tower dragTower;
    public TowerData dropTower;
    private void Awake()
    {
        detectedTowerData = null;
        dragTower = new Tower();
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
        if (Input.GetMouseButtonDown(0) && detectedTowerData.towerID == null)
        {
            if (detectedTowerGroundData != null && detectedTowerGroundData.towerData.towerID != "")
            {
                detectedTowerData = detectedTowerGroundData.towerData;
                detectedTowerGroundData.RemoveTower();

                //dragTower.towerData = detectedTowerGroundData.towerData;
                //detectedTowerID = dragTower.towerData.towerID;
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
        if(detectedTowerGroundData == null)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0) && detectedTowerData.towerID != null) //무언가 이동 중이라면
        {
            //현재 그라운드데이터에 타워가 있는 경우
              //같은 좋류, 레벨의 타워라면
              //다른 종류의 타워라면

            //현재 그라운드에티어에 타워가 없는 경우
            if(detectedTowerGroundData.towerData.towerID == null )
            {
                detectedTowerGroundData.towerData = detectedTowerData;
                detectedTowerData = null;
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

            if (towerGround != null && !towerGround.ISHasTower())
            {
                if (detectedTowerGroundData != null)
                {
                    detectedTowerGroundData.ExitTowerGround(detectedTowerGroundData);
                }
                this.detectedTowerGroundData = towerGround.towerGroundData;

                isFindGround = true;
                detectedTowerGroundData.EnterTowerGround(detectedTowerGroundData);
                break;
            }
            if (towerGround != null && towerGround.ISHasTower())
            {
                detectedTowerGroundData.ExitTowerGround(detectedTowerGroundData);
            }
        }

        if (!isFindGround)
        {
            if (detectedTowerGroundData != null)
            {
                detectedTowerGroundData.ExitTowerGround(detectedTowerGroundData);
            }
            detectedTowerGroundData = null;

        }
    }
}
