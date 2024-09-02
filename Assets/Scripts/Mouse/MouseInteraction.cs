using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    TowerGroundManager towerGroundManager;
    public GameObject towerPopupPrefab;
    public RectTransform popupTransform;
    TowerManagerPopup towerPopup;

    public event Action<TowerGroundData> inMouseOnGround;
    public event Action<TowerGroundData> outMouseOnGround;

    public event Action<TowerGroundData> dragTower;
    public event Action<TowerGroundData> dropTower;

    public event Action openPopupInfo;
    public event Action closePopupInfo;
    private void Start()
    {
        towerGroundManager = TowerGroundManager.instance;
    }
    private void Update()
    {
        ScreenToRayUseMouse();
        MouseButtonDown();
        MouseButtonDrag();
        MouseButtonUP();
    }   
    void ScreenToRayUseMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        bool isFindGround = false;
        foreach (RaycastHit hit in hits)
        {
            if(hit.collider.CompareTag("UI"))
            {
                return;
            }
            TowerGround towerGround = hit.collider.GetComponent<TowerGround>();

            if (towerGround != null)
            {
                if (towerGroundManager.detectedTowerGroundData != null)
                {
                    outMouseOnGround?.Invoke(towerGroundManager.detectedTowerGroundData);
                }
                towerGroundManager.detectedTowerGroundData = towerGround.towerGroundData;
                 
                isFindGround = true;
                inMouseOnGround?.Invoke(towerGroundManager.detectedTowerGroundData);
                break;
            }
        }
        if(isFindGround && towerGroundManager.detectedTowerGroundData.towerData != null) //여기서 팝업 생성
        {
            if(towerPopup == null)
            {
                //towerPopup = Instantiate(towerPopupPrefab, popupTransform).GetComponent<TowerManagerPopup>();
            }
            else
            {

            }
        }
        if (!isFindGround)
        {
            if (towerGroundManager.detectedTowerGroundData != null)
            {
                outMouseOnGround?.Invoke(towerGroundManager.detectedTowerGroundData);
            }
            towerGroundManager.detectedTowerGroundData = null;
        }
    }
    void MouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.LogError("MouseButtonDown : 마우스 드래그 누름");
        }
    }
    void MouseButtonDrag()
    {
         if (Input.GetMouseButton(0))
        {
            Debug.LogError("MouseButton : 마우스 드래그 중");
        }
    }

    void MouseButtonUP()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.LogError("MouseButtonUp : 마우스 띄워짐");
        }
    }
    void MoveTower(GameObject obj)
    {

    }
}
//public class MouseInteraction : MonoBehaviour
//{
//    public GameObject towerPopupPrefab;
//    public RectTransform popupTransform;
//    TowerManagerPopup towerPopup;

//    TowerEventHandler towerEventHandler; // 타워관련 이벤트 쏘는 역할
//    public Tower dragTower;
//    public TowerData dropTower;
//    private void Start()
//    {
//        towerEventHandler = TowerGroundManager.instance.towerEventHandler;
//    }
//    private void Update()
//    {
//        MouseButtonDown();
//        MouseButtonUP();
//        ScreenToRayUseMouse();
//        MoveTower();
//    }
//    void MoveTower()
//    {
//        if (dragTower != null)
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
//            float rayDistance;

//            if (groundPlane.Raycast(ray, out rayDistance))
//            {
//                Vector3 point = ray.GetPoint(rayDistance);
//                Vector3 adjustPoint = new Vector3(point.x, 1, point.z);
//                dragTower.transform.position = adjustPoint;
//            }
//        }
//    }
//    void MouseButtonDown()
//    {
//        //여기 에러 잡아야함
//        //마우스 광클릭 했을 때 null이 나올때가 있고 아닐 때가 있음
//        Debug.Log(towerEventHandler.detectedSelectTowerData);
//        bool isClick = false;
//        if (Input.GetMouseButtonDown(0) && towerEventHandler.detectedSelectTowerData == null) // 선택된 타워 데이터가 없는 경우
//        {
//            isClick = true;
//            if (towerEventHandler.detectedCurrentTowerGroundData != null && towerEventHandler.detectedCurrentTowerGroundData.towerData != null)
//            {
//                towerEventHandler.detectedSelectTowerGroundData = towerEventHandler.detectedCurrentTowerGroundData; //클릭 시 이전 데이터를 저장하기 위한 용도
//                towerEventHandler.detectedSelectTowerData = towerEventHandler.detectedCurrentTowerGroundData.towerData;
//                towerEventHandler.detectedCurrentTowerGroundData.RemoveTower();
//            }
//            else
//            {
//                Debug.Log("그라운드에 타워가 없음");
//                return;
//            }
//        }
//        if (Input.GetMouseButtonDown(0) && towerEventHandler.detectedSelectTowerData != null && !isClick) // 선택된 타워 데이터가 있는 경우
//        {
//            OnSelectFieldTower(towerEventHandler.detectedSelectTowerData);
//            if (towerEventHandler.detectedCurrentTowerGroundData != null && towerEventHandler.detectedCurrentTowerGroundData.towerData != null)
//            {
//                towerEventHandler.detectedSelectTowerData = towerEventHandler.detectedCurrentTowerGroundData.towerData;
//                towerEventHandler.detectedCurrentTowerGroundData.RemoveTower();
//            }
//            else
//            {
//                Debug.Log("그라운드에 타워가 없음");
//                return;
//            }
//        }
//    }

//    // field 선택했을때
//    public void OnSelectFieldTower(TowerData towerData)
//    {
//        if (towerEventHandler.detectedCurrentTowerGroundData != null && towerEventHandler.detectedCurrentTowerGroundData.towerData != null)
//        {
//            towerEventHandler.detectedSelectTowerData = towerEventHandler.detectedCurrentTowerGroundData.towerData;
//            towerEventHandler.detectedCurrentTowerGroundData.RemoveTower();
//        }
//        else
//        {
//            Debug.Log("그라운드에 타워가 없음");
//            return;
//        }
//    }

//    // 상점 선택했을때
//    public void OnSelectShopTower(TowerData towerData)
//    {

//    }

//    void MouseButtonUP()
//    {
//        if (towerEventHandler.detectedCurrentTowerGroundData == null)
//        {
//            return;
//        }
//        if (Input.GetMouseButtonUp(0) && towerEventHandler.detectedSelectTowerData != null) //무언가 이동 중이라면
//        {
//            //현재 그라운드데이터에 타워가 있는 경우
//            //같은 좋류, 레벨의 타워라면
//            //다른 종류의 타워라면

//            //현재 그라운드에티어에 타워가 없는 경우
//            if (towerEventHandler.detectedCurrentTowerGroundData.towerData.towerID == null)
//            {
//                towerEventHandler.SetTower(towerEventHandler.detectedCurrentTowerGroundData, towerEventHandler.detectedSelectTowerData);
//                towerEventHandler.detectedCurrentTowerGroundData.towerData = towerEventHandler.detectedSelectTowerData;
//                towerEventHandler.detectedSelectTowerData = null;
//            }
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

//            if (towerGround != null)
//            {
//                if (towerEventHandler.detectedCurrentTowerGroundData != null)
//                {
//                    towerEventHandler.ExitTowerGround(towerEventHandler.detectedCurrentTowerGroundData);
//                }
//                towerEventHandler.detectedCurrentTowerGroundData = towerGround.towerGroundData;

//                isFindGround = true;
//                towerEventHandler.EnterTowerGround(towerEventHandler.detectedCurrentTowerGroundData);
//                break;
//            }
//        }
//        if (isFindGround && towerEventHandler.detectedCurrentTowerGroundData.towerData != null) //여기서 팝업 생성
//        {
//            if (towerPopup == null)
//            {
//                towerPopup = Instantiate(towerPopupPrefab, popupTransform).GetComponent<TowerManagerPopup>();
//            }
//            else
//            {

//            }
//        }
//        if (!isFindGround)
//        {
//            if (towerEventHandler.detectedCurrentTowerGroundData != null)
//            {
//                towerEventHandler.ExitTowerGround(towerEventHandler.detectedCurrentTowerGroundData);
//            }
//            towerEventHandler.detectedCurrentTowerGroundData = null;
//        }
//    }
//}