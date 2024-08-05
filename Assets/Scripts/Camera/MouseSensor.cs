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

            if (towerGround != null && !towerGround.ISHasTower()) //타워가 배치되어 있지 않을 때
            {
                Debug.Log("TowerGround 확인");
                GameManager.instance.towerGroundEventManager.MouseExit(this.towerGround);
                this.towerGround = towerGround;
                isFindGround = true;
                GameManager.instance.towerGroundEventManager.MouseEnter(this.towerGround);
                if(Input.GetMouseButton(0) && haveTower != HaveTowerData.None)
                {
                    //직전 그라운드 프리펩 제거(직전 그라운드 프리펩 있으면 이동하는 애니메이션?)
                    //놓은 곳 그라운드에 프리펩 생성
                }
                break;
            }
            if(towerGround != null && towerGround.ISHasTower())//타워가 배치되어 있을 때
            {
                GameManager.instance.towerGroundEventManager.MouseExit(this.towerGround);
                //기존 타워와 위치를 바꿀 때
                if(haveTower == HaveTowerData.GroundTowerData)
                {

                }
                //상점에서 타워를 구매해서 배치할 때
                if(haveTower == HaveTowerData.ShopTowerData)
                {
                    //아래 동그라미 빨간색으로 바뀌고 return하게 끔 하면 될듯
                }                
                break;
            }
        }
        if (!isFindGround) //어떠한 것도 검출되어있지 않다면 원상복귀
        {
            GameManager.instance.towerGroundEventManager.MouseExit(towerGround);
            towerGround = null;
        }
    }
}