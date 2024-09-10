using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInteraction : MonoBehaviour
{
    public static MouseInteraction instance;
    TowerGroundManager towerGroundManager;

    public GameObject towerPopupPrefab;
    public RectTransform popupTransform;
    TowerManagerPopup towerPopup;

    event Action onBuyTowerDropped;//



    GameObject dragObj;
    TowerGround detectedTowerGround;
    TowerGround clickTowerGround;
    TowerData boughtShopTowerData;
    public event Action<TowerGround, TowerData> dropBuyTowerOnGround;
    public event Action<TowerGround> inMouseOnGround;
    public event Action<TowerGround> outMouseOnGround;
    bool isBuingTower = false;//
    bool isMouseOnGround = false;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        towerGroundManager = TowerGroundManager.instance;

        EventManager.instance.onBuyShopTower += BuingTower;
    }
    private void Update()
    {
        MouseButtonDown();
        ScreenToRayUseMouse();
        MouseButtonUp();
        if (isBuingTower)
        {
            DragTowerObj(dragObj, boughtShopTowerData);
        }
    }
    void ScreenToRayUseMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        bool isFindGround = false;
        foreach (RaycastHit hit in hits)
        {
            if (IsPointerInUI())
            {
                isMouseOnGround = false;
                break;
            }

            TowerGround towerGround = hit.collider.GetComponent<TowerGround>();
            if (towerGround != null)
            {
                if (detectedTowerGround != null)
                {
                    outMouseOnGround?.Invoke(detectedTowerGround);
                    isMouseOnGround = false;
                    //detectedTowerGround = null;
                    // detectedTowerGround.towerGroundData = towerGround.towerGroundData;
                }
                detectedTowerGround = null;//
                detectedTowerGround = towerGround;
                isFindGround = true;
                inMouseOnGround?.Invoke(detectedTowerGround);
                isMouseOnGround = true;
                break;
            }
        }
        if (isFindGround && detectedTowerGround.towerGroundData.towerData != null) //여기서 팝업 생성
        {
            if (towerPopup == null)
            {
                //towerPopup = Instantiate(towerPopupPrefab, popupTransform).GetComponent<TowerManagerPopup>();
            }
            else
            {

            }
        }
        if (!isFindGround)
        {
            if (detectedTowerGround != null && detectedTowerGround.towerGroundData != null)
            {
                outMouseOnGround?.Invoke(detectedTowerGround);
                isMouseOnGround = false;
            }
            detectedTowerGround = null;
        }
    }

    bool IsPointerInUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        return results.Count > 0;
    }
    void MouseButtonDown()
    {
        //필드 선택(구매한 타워가 없고 그라운드 내 마우스가 있으며 선택된 타워그라운드가 없을 때)
        //필드 타워 선택
        if (Input.GetMouseButtonDown(0) && !isBuingTower && isMouseOnGround && clickTowerGround == null)
        {
            if (detectedTowerGround != null)
            {
                clickTowerGround = detectedTowerGround;
            }
            if(clickTowerGround.towerGroundData.towerData == null)
            {
                clickTowerGround = null;
                return;
            }
        }

        if(Input.GetMouseButtonDown(0))
        {

        }

        if (Input.GetMouseButtonDown(0) && !isBuingTower && isMouseOnGround && clickTowerGround != null && clickTowerGround.towerGroundData != null
            && clickTowerGround.towerGroundData.towerData != null && clickTowerGround.towerGroundData.towerData.towerID =="nor01")
        {
            Debug.LogError("팝업 띄우고 바로 랜덤 돌릴 수 있게");
            clickTowerGround = null;
        }
        //필드 선택(구매한 타워가 없고 그라운드 내 마우스가 있으며 선택된 타워그라운드가 있을 때)

        //첫번째 선택된 타워와 두번째 선택된 타워가 같다면

        //첫번째 선택된 타워와 두번째 선택된 타워가 다르다면
        if (Input.GetMouseButtonDown(0) && !isBuingTower && isMouseOnGround && clickTowerGround != null && clickTowerGround.towerGroundData != null && clickTowerGround.towerGroundData.towerData != null)
        {
            if (clickTowerGround.towerGroundData.towerGroundNum == detectedTowerGround.towerGroundData.towerGroundNum)
            {
                return;
            }
            else
            {
                //속성타입과 레벨이 같다면
                if (clickTowerGround.towerGroundData.towerData.status.elementaProperties == detectedTowerGround.towerGroundData.towerData.status.elementaProperties &&
                    clickTowerGround.towerGroundData.towerData.status.level == detectedTowerGround.towerGroundData.towerData.status.level)
                {
                    Debug.LogError("타입과 레벨 같음 ! UI에서 합체 버튼 활성화");
                    clickTowerGround = null;
                }
                else
                {
                    Debug.LogError("타입 또는 레벨이 다름 ! 꺼지게");
                    clickTowerGround = null;
                    return;
                }
            }
        }
        //그라운드가 아닌 곳에서 클릭하면 초기화
        if (Input.GetMouseButtonDown(0) && !isBuingTower && !isMouseOnGround)
        {
            clickTowerGround = null;
        }

        //상점 구매 타워 드랍
        if (Input.GetMouseButtonDown(0) && isBuingTower && isMouseOnGround)
        {
            dropBuyTowerOnGround?.Invoke(detectedTowerGround, boughtShopTowerData);
            EventManager.instance.DropTowerForDraggableTowerClearData(); // DraggableTower에서 선택된 데이터 초기화
            boughtShopTowerData = null;
            isBuingTower = false;
        }
    }
    void MouseButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
        }
    }
    public void DragTowerObj(GameObject obj, TowerData data)
    {
        if (obj != null)
        {
            dragObj = obj;
            Vector3 mousePosition = CurrentMousePos();
            obj.transform.position = mousePosition;
            boughtShopTowerData = data;
        }
        else
        {
            dragObj = null;
            Debug.LogError("드랍이나 취소 데이터 없음");
        }
    }
    public Vector3 CurrentMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 adjustPoint = new Vector3(point.x, 1, point.z);
            return adjustPoint;
        }
        else
        {
            Debug.LogError("으악!~!!~");
            return Vector3.zero;
        }
    }
    public void BuingTower()
    {
        isBuingTower = true;
    }
}