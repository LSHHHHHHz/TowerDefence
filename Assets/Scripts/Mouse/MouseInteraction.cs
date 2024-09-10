using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInteraction : MonoBehaviour
{
    public static MouseInteraction instance;
    TowerGroundManager towerGroundManager;
    TowerPlacementManager towerPlaceManager;

    public GameObject towerPopupPrefab;
    public RectTransform popupTransform;
    TowerManagerPopup towerPopup;


    event Action<TowerGround> selectTowerGround;
    public event Action<TowerData> selectTowerData;
    event Action clearData;
    TowerGround clickTowerGround;
    bool isMouseOnGround = false;
    bool hasTwoSlectedTower = false;
    bool isMergeTower = false;
    bool isSameTowerGround = false;
    bool isclickTowerGround = false;

    event Action onBuyTowerDropped;//



    GameObject dragObj;
    TowerGround detectedTowerGround = new TowerGround();
    TowerData boughtShopTowerData;
    public event Action<TowerGround, TowerData> dropBuyTowerOnGround;
    public event Action<TowerGround> inMouseOnGround;
    public event Action<TowerGround> outMouseOnGround;
    bool isBuingTower = false;//
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        towerGroundManager = TowerGroundManager.instance;
        towerPlaceManager = GameData.instance.towerManager;
        onBuyTowerDropped += towerPlaceManager.BuyTowerDropOnGround;
        selectTowerData += towerPlaceManager.RegisterTowerData;
        clearData += towerPlaceManager.BuyTowerDropOnGround;
        clearData += towerGroundManager.ClearData;        
        selectTowerGround += towerGroundManager.SetCurrentSelectedGround;
        towerPlaceManager.clickTwoTower += SelectedTwoTower;
        towerPlaceManager.canMergeTower += CanMergeTower;

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
                this.clickTowerGround = towerGround;
                if (detectedTowerGround != null)
                {
                    outMouseOnGround?.Invoke(detectedTowerGround);
                    isMouseOnGround = false;
                detectedTowerGround.towerGroundData = towerGround.towerGroundData;
                }
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
        //상점 구매 타워 드랍
        if (Input.GetMouseButtonDown(0) && isBuingTower && isMouseOnGround)
        {
            EventManager.instance.DropTowerForDraggableTowerClearData(); // DraggableTower에서 선택된 데이터 초기화
            dropBuyTowerOnGround?.Invoke(detectedTowerGround, boughtShopTowerData);
            boughtShopTowerData = null;
            isBuingTower = false;
        }

        //필드 선택

        //필드 타워 선택

        //선택된 타워가 두개가 있을 경우

        //첫번째 선택된 타워와 두번째 선택된 타워가 같다면

        //첫번째 선택된 타워와 두번째 선택된 타워가 다르다면



        //그라운드가 아닌 곳에서 클릭하면 초기화
    }
    void MouseButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isclickTowerGround = false;
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
    private void CanMergeTower(bool canMerge)
    {
        isMergeTower = canMerge;
    }
    public void SelectedTwoTower()
    {
        hasTwoSlectedTower = true;
    }
    public void BuingTower()
    {
        isBuingTower = true;
    }    
}