using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum HaveTowerData
{
    None,
    ShopTowerData,
    GroundTowerData
}
public class MouseSensor : MonoBehaviour
{
    TowerGround towerGround;
    Tower preSelectTower;
    Tower currentSelectTower;
    HaveTowerData haveTower;

    private void Update()
    {
        ScreenToRayUseMouse();
        SelectTower();
    }
    void SelectTower()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach(RaycastHit hit in hits )
            {
                Tower tower = hit.collider.GetComponent<Tower>();
                if(tower != null && preSelectTower == null)
                {
                    this.preSelectTower = tower;
                    return;
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
                Debug.Log("TowerGround Ȯ��");
                GameManager.instance.towerGroundEventManager.MouseExit(this.towerGround);
                this.towerGround = towerGround;
                isFindGround = true;
                GameManager.instance.towerGroundEventManager.MouseEnter(this.towerGround);
                if(Input.GetMouseButton(0) && haveTower != HaveTowerData.None)
                {
                    //���� �׶��� ������ ����(���� �׶��� ������ ������ �̵��ϴ� �ִϸ��̼�?)
                    //���� �� �׶��忡 ������ ����
                }
                break;
            }
            if(towerGround != null && towerGround.ISHasTower())//Ÿ���� ��ġ�Ǿ� ���� ��
            {
                GameManager.instance.towerGroundEventManager.MouseExit(this.towerGround);
                //���� Ÿ���� ��ġ�� �ٲ� ��
                if(haveTower == HaveTowerData.GroundTowerData)
                {

                }
                //�������� Ÿ���� �����ؼ� ��ġ�� ��
                if(haveTower == HaveTowerData.ShopTowerData)
                {
                    //�Ʒ� ���׶�� ���������� �ٲ�� return�ϰ� �� �ϸ� �ɵ�
                }                
                break;
            }
        }
        if (!isFindGround) //��� �͵� ����Ǿ����� �ʴٸ� ���󺹱�
        {
            GameManager.instance.towerGroundEventManager.MouseExit(towerGround);
            towerGround = null;
        }
    }
}