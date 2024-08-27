using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MouseSensor : MonoBehaviour
{
    public TowerGround detectedTowerGround;
    public string detectedTowerID;
    public Tower dragTower;
    public Tower dropTower;

    private void Update()
    {
        MouseButtonDown();
        MouseButtonUP();
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
    }
    void MouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (detectedTowerGround != null && detectedTowerGround.currentTower != null)
            {
                dragTower = detectedTowerGround.currentTower;
                detectedTowerID = detectedTowerGround.currentTowerID;
                detectedTowerGround.currentTower = null;
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
        if (Input.GetMouseButtonUp(0) && dragTower != null)//드래그 중인 타워가 있으면서
        {
            // Ground에 타워가 있는 경우
            if (detectedTowerGround.currentTower != null)
            {
                //타워의 ID가 같을 경우 (윗 레벨로 업그레이드)

                //타워의 ID가 다를 경우 (위치 교환)
            }
            //Ground에 타워가 없는 경우
            else
            {
                detectedTowerGround.towerGroundData.SetTower(detectedTowerID, ActorType.ChampionTower); //타입도 코드로 작성
                //detectedTowerGround.currentTower = dragTower;
                //detectedTowerGround.currentTower.transform.position = detectedTowerGround.transform.position + new Vector3(0,1,0);
                dragTower = null;
            }
        }
    }
    void PickUpTower()
    {

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
                GameManager.instance.towerGroundEventManager.MouseExit(this.detectedTowerGround);
                this.detectedTowerGround = towerGround;
                isFindGround = true;
                GameManager.instance.towerGroundEventManager.MouseEnter(this.detectedTowerGround);
                break;
            }
            if (towerGround != null && towerGround.ISHasTower())//타워가 배치되어 있을 때
            {
                GameManager.instance.towerGroundEventManager.MouseExit(this.detectedTowerGround);
            }
        }
        if (!isFindGround) //어떠한 것도 검출되어있지 않다면 원상복귀
        {
            GameManager.instance.towerGroundEventManager.MouseExit(detectedTowerGround);
            detectedTowerGround = null;
        }
    }
}