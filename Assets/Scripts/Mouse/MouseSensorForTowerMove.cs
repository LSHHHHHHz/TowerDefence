//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using Unity.VisualScripting;
//using UnityEngine;
//using static UnityEditor.PlayerSettings;

//// ���� ����
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
//    //ó�� Ÿ���� ����
//    //����� Ÿ���� �ִٸ�
//        //���� ���� Ÿ���� �ִ���
//        //          Ÿ���� ������
        
//    void DragTower() //�巡�� �� Ÿ���� ���� ��
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
//    void DropTower() //�巡�� �� Ÿ���� ���� ��
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
//                    if (ground != null && ground.currentTower != null && ground.currentTower != dragTower) //Ÿ���� ���� ���
//                    {
//                        dropTowerGround = ground;
//                        ground.towerGroundData.SetTower(dragTowerGround.currentTower.actorId, dragTowerGround.currentTower.actoryType);
//                        dragTowerGround.towerGroundData.SetTower(dropTowerGround.currentTower.actorId, dropTowerGround.currentTower.actoryType);
//                        dragTowerGround = null;
//                        dropTowerGround = null;
//                        break;
//                    }
//                    if (ground != null && ground.currentTower == null) // Ÿ���� ���� ���
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
//        if (dragTower != null) //Ÿ���� �ִٸ� �ش� Ÿ�� ���콺�� ���� �̵�
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
//        if (Input.GetMouseButtonUp(0) && dragTower != null) //���콺�� ������ �� �ʱ�ȭ
//        {
//            //���ο� ���� ��ġ�� ���� ������ ���� ��ġ�� �ٽ� �̵�
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

//            if (towerGround != null && !towerGround.ISHasTower()) //Ÿ���� ��ġ�Ǿ� ���� ���� ��
//            {
//                GameManager.instance.towerGroundEventManager.MouseExit(groundColor);
//                groundColor = towerGround;
//                isFindGround = true;
//                GameManager.instance.towerGroundEventManager.MouseEnter(groundColor);
//                break;
//            }
//            if (towerGround != null && towerGround.ISHasTower())//Ÿ���� ��ġ�Ǿ� ���� ��
//            {
//                GameManager.instance.towerGroundEventManager.MouseExit(groundColor);
//            }
//        }
//        if (!isFindGround) //��� �͵� ����Ǿ����� �ʴٸ� ���󺹱�
//        {
//            GameManager.instance.towerGroundEventManager.MouseExit(groundColor);
//            groundColor = null;
//        }
//    }
//}