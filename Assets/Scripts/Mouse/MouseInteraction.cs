using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    public GameObject towerPopupPrefab;
    public RectTransform popupTransform;
    TowerManagerPopup towerPopup;

    TowerEventHandler towerEventHandler;
    public Tower dragTower;
    public TowerData dropTower;
    private void Start()
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
        //���� ���� ��ƾ���
        //���콺 ��Ŭ�� ���� �� null�� ���ö��� �ְ� �ƴ� ���� ����
        Debug.Log(towerEventHandler.detectedSelectTowerData);
        bool isClick = false;
        if (Input.GetMouseButtonDown(0) && towerEventHandler.detectedSelectTowerData == null) // ���õ� Ÿ�� �����Ͱ� ���� ���
        {
            isClick = true;
            if (towerEventHandler.detectedCurrentTowerGroundData != null && towerEventHandler.detectedCurrentTowerGroundData.towerData != null)
            {
                towerEventHandler.detectedSelectTowerGroundData = towerEventHandler.detectedCurrentTowerGroundData; //Ŭ�� �� ���� �����͸� �����ϱ� ���� �뵵
                towerEventHandler.detectedSelectTowerData = towerEventHandler.detectedCurrentTowerGroundData.towerData;
                towerEventHandler.detectedCurrentTowerGroundData.RemoveTower();
            }
            else
            {
                Debug.Log("�׶��忡 Ÿ���� ����");
                return;
            }
        }
        if (Input.GetMouseButtonDown(0) && towerEventHandler.detectedSelectTowerData != null && !isClick) // ���õ� Ÿ�� �����Ͱ� �ִ� ���
        {
            if (towerEventHandler.detectedCurrentTowerGroundData != null && towerEventHandler.detectedCurrentTowerGroundData.towerData != null)
            {
                towerEventHandler.detectedSelectTowerData = towerEventHandler.detectedCurrentTowerGroundData.towerData;
                towerEventHandler.detectedCurrentTowerGroundData.RemoveTower();
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
        if (towerEventHandler.detectedCurrentTowerGroundData == null)
        {
            return;
        }
        if (Input.GetMouseButtonUp(0) && towerEventHandler.detectedSelectTowerData != null) //���� �̵� ���̶��
        {
            //���� �׶��嵥���Ϳ� Ÿ���� �ִ� ���
            //���� ����, ������ Ÿ�����
            //�ٸ� ������ Ÿ�����

            //���� �׶��忡Ƽ� Ÿ���� ���� ���
            if (towerEventHandler.detectedCurrentTowerGroundData.towerData.towerID == null)
            {
                towerEventHandler.SetTower(towerEventHandler.detectedCurrentTowerGroundData, towerEventHandler.detectedSelectTowerData);
                towerEventHandler.detectedCurrentTowerGroundData.towerData = towerEventHandler.detectedSelectTowerData;
                towerEventHandler.detectedSelectTowerData = null;
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

            if (towerGround != null)
            {
                if (towerEventHandler.detectedCurrentTowerGroundData != null)
                {
                    towerEventHandler.ExitTowerGround(towerEventHandler.detectedCurrentTowerGroundData);
                }
                towerEventHandler.detectedCurrentTowerGroundData = towerGround.towerGroundData;
                 
                isFindGround = true;
                towerEventHandler.EnterTowerGround(towerEventHandler.detectedCurrentTowerGroundData);
                break;
            }
        }
        if(isFindGround && towerEventHandler.detectedCurrentTowerGroundData.towerData != null) //���⼭ �˾� ����
        {
            if(towerPopup == null)
            {
                towerPopup = Instantiate(towerPopupPrefab, popupTransform).GetComponent<TowerManagerPopup>();
            }
            else
            {

            }
        }
        if (!isFindGround)
        {
            if (towerEventHandler.detectedCurrentTowerGroundData != null)
            {
                towerEventHandler.ExitTowerGround(towerEventHandler.detectedCurrentTowerGroundData);
            }
            towerEventHandler.detectedCurrentTowerGroundData = null;
        }
    }
}
