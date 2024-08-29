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
        if (Input.GetMouseButtonDown(0) && towerEventHandler.detectedTowerData == null) // ���õ� Ÿ�� �����Ͱ� ���� ���
        {
            isClick = true;
            if (towerEventHandler.detectedTowerGroundData != null && towerEventHandler.detectedTowerGroundData.towerData != null)
            {
                towerEventHandler.detectedTowerData = towerEventHandler.detectedTowerGroundData.towerData;
                towerEventHandler.detectedTowerGroundData.RemoveTower();
            }
            else
            {
                Debug.Log("�׶��忡 Ÿ���� ����");
                return;
            }
        }
        if (Input.GetMouseButtonDown(0) && towerEventHandler.detectedTowerData != null && !isClick) // ���õ� Ÿ�� �����Ͱ� �ִ� ���
        {
            if (towerEventHandler.detectedTowerGroundData != null && towerEventHandler.detectedTowerGroundData.towerData != null)
            {
                towerEventHandler.detectedTowerData = towerEventHandler.detectedTowerGroundData.towerData;
                towerEventHandler.detectedTowerGroundData.RemoveTower();
            }
            else
            {
                Debug.Log("�׶��忡 Ÿ���� ����");
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

        if (Input.GetMouseButtonUp(0) && towerEventHandler.detectedTowerData != null) //���� �̵� ���̶��
        {
            //���� �׶��嵥���Ϳ� Ÿ���� �ִ� ���
            //���� ����, ������ Ÿ�����
            //�ٸ� ������ Ÿ�����

            //���� �׶��忡Ƽ� Ÿ���� ���� ���
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
