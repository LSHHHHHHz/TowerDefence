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
        if (dragTower != null) //Ÿ���� �ִٸ� �ش� Ÿ�� ���콺�� ���� �̵�
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
        if (Input.GetMouseButtonUp(0) && dragTower != null) //���콺�� ������ �� �ʱ�ȭ
        {
            //���ο� ���� ��ġ�� ���� ������ ���� ��ġ�� �ٽ� �̵�
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

            if (towerGround != null && !towerGround.ISHasTower()) //Ÿ���� ��ġ�Ǿ� ���� ���� ��
            {
                GameManager.instance.towerGroundEventManager.MouseExit(groundColor);
                groundColor = towerGround;
                isFindGround = true;
                GameManager.instance.towerGroundEventManager.MouseEnter(groundColor);
                break;
            }
            if (towerGround != null && towerGround.ISHasTower())//Ÿ���� ��ġ�Ǿ� ���� ��
            {
                GameManager.instance.towerGroundEventManager.MouseExit(groundColor);
            }
        }
        if (!isFindGround) //��� �͵� ����Ǿ����� �ʴٸ� ���󺹱�
        {
            GameManager.instance.towerGroundEventManager.MouseExit(groundColor);
            groundColor = null;
        }
    }
}