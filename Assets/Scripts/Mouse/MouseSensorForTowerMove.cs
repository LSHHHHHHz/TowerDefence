//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using Unity.VisualScripting;
//using UnityEngine;
//using static UnityEditor.PlayerSettings;

//// 삭제 예정
//public enum HaveTowerData
//{
//    None,
//    ShopTowerData,
//    GroundTowerData
//}
//public class MouseSensorForTowerMove : MonoBehaviour
//{
//    public TowerGround groundColor;
//    public TowerGround dragTowerGround;
//    public TowerGround dropTowerGround;
//    public Tower dragTower;
//    public Tower dropTower;
//    HaveTowerData haveTower;

//    private void Update()
//    {
//        DragTower();
//        DropTower();

//        ScreenToRayUseMouse();
//        MoveTower();
//    }
//    //처음 타워를 검출
//    //검출된 타워가 있다면
//        //놓을 곳에 타워가 있는지
//        //          타워가 없는지
        
//    void DragTower() //드래그 할 타워가 없을 때
//    {
//        if (Input.GetMouseButtonDown(0) && dragTower ==null)
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit[] hits = Physics.RaycastAll(ray);
//            foreach (RaycastHit hit in hits)
//            {
//                if(hit.collider != null)
//                {
//                    TowerGround ground = hit.collider.GetComponent<TowerGround>();
//                    if(ground != null && ground.currentTower != null)
//                    {
//                        dragTowerGround = ground;
//                        dragTower = dragTowerGround.currentTower;
//                        break;
//                    }
//                }
//            }
//        }
//    }
//    void DropTower() //드래그 할 타워가 있을 때
//    {
//        if (Input.GetMouseButtonUp(0) && dragTower != null)
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit[] hits = Physics.RaycastAll(ray);
//            foreach (RaycastHit hit in hits)
//            {
//                if (hit.collider != null)
//                {
//                    TowerGround ground = hit.collider.GetComponent<TowerGround>();
//                    if (ground != null && ground.currentTower != null && ground.currentTower != dragTower) //타워가 있을 경우
//                    {
//                        dropTowerGround = ground;
//                        ground.towerGroundData.SetTower(dragTowerGround.currentTower.actorId, dragTowerGround.currentTower.actoryType);
//                        dragTowerGround.towerGroundData.SetTower(dropTowerGround.currentTower.actorId, dropTowerGround.currentTower.actoryType);
//                        dragTowerGround = null;
//                        dropTowerGround = null;
//                        break;
//                    }
//                    if (ground != null && ground.currentTower == null) // 타워가 없을 경우
//                    {
//                        ground.towerGroundData.SetTower(dragTowerGround.currentTower.actorId, dragTowerGround.currentTower.actoryType);
//                        dragTowerGround.towerGroundData.RemoveTower();
//                        break;
//                    }
//                }
//            }
//        }
//    }
//    void MoveTower()
//    {
//        if (dragTower != null) //타워가 있다면 해당 타워 마우스랑 같이 이동
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
//            float rayDistance;

//            if (groundPlane.Raycast(ray, out rayDistance))
//            {
//                Vector3 point = ray.GetPoint(rayDistance);
//                dragTower.transform.position = point;
//            }
//        }
//        if (Input.GetMouseButtonUp(0) && dragTower != null) //마우스를 놓았을 때 초기화
//        {
//            //새로운 곳에 배치가 되지 않으면 기존 위치로 다시 이동
//            dragTower = null;
//            haveTower = HaveTowerData.None;
//        }
//    }
//    void ScreenToRayUseMouse()
//    {
//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        RaycastHit[] hits = Physics.RaycastAll(ray);
//        bool isFindGround = false;

//        foreach (RaycastHit hit in hits)
//        {
//            TowerGround towerGround = hit.collider.GetComponent<TowerGround>();

//            if (towerGround != null && !towerGround.ISHasTower()) //타워가 배치되어 있지 않을 때
//            {
//                GameManager.instance.towerGroundEventManager.MouseExit(groundColor);
//                groundColor = towerGround;
//                isFindGround = true;
//                GameManager.instance.towerGroundEventManager.MouseEnter(groundColor);
//                break;
//            }
//            if (towerGround != null && towerGround.ISHasTower())//타워가 배치되어 있을 때
//            {
//                GameManager.instance.towerGroundEventManager.MouseExit(groundColor);
//            }
//        }
//        if (!isFindGround) //어떠한 것도 검출되어있지 않다면 원상복귀
//        {
//            GameManager.instance.towerGroundEventManager.MouseExit(groundColor);
//            groundColor = null;
//        }
//    }
//}