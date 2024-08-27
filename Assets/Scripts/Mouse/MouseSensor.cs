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
                Debug.Log("�׶��忡 Ÿ���� ����");
                return;
            }
        }
    }
    void MouseButtonUP()
    {
        if (Input.GetMouseButtonUp(0) && dragTower != null)//�巡�� ���� Ÿ���� �����鼭
        {
            // Ground�� Ÿ���� �ִ� ���
            if (detectedTowerGround.currentTower != null)
            {
                //Ÿ���� ID�� ���� ��� (�� ������ ���׷��̵�)

                //Ÿ���� ID�� �ٸ� ��� (��ġ ��ȯ)
            }
            //Ground�� Ÿ���� ���� ���
            else
            {
                detectedTowerGround.towerGroundData.SetTower(detectedTowerID, ActorType.ChampionTower); //Ÿ�Ե� �ڵ�� �ۼ�
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

            if (towerGround != null && !towerGround.ISHasTower()) //Ÿ���� ��ġ�Ǿ� ���� ���� ��
            {
                GameManager.instance.towerGroundEventManager.MouseExit(this.detectedTowerGround);
                this.detectedTowerGround = towerGround;
                isFindGround = true;
                GameManager.instance.towerGroundEventManager.MouseEnter(this.detectedTowerGround);
                break;
            }
            if (towerGround != null && towerGround.ISHasTower())//Ÿ���� ��ġ�Ǿ� ���� ��
            {
                GameManager.instance.towerGroundEventManager.MouseExit(this.detectedTowerGround);
            }
        }
        if (!isFindGround) //��� �͵� ����Ǿ����� �ʴٸ� ���󺹱�
        {
            GameManager.instance.towerGroundEventManager.MouseExit(detectedTowerGround);
            detectedTowerGround = null;
        }
    }
}