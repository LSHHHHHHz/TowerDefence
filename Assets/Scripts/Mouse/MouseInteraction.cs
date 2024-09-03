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

    public event Action<TowerGroundData> inMouseOnGround;
    public event Action<TowerGroundData> outMouseOnGround;

    event Action<TowerGround> selectTowerGround;
    public event Action<TowerData> selectTowerData;
    event Action clearData;
    TowerGround clickTowerGround;
    bool isMouseOnGround = false;
    bool hasTwoSlectedTower = false;
    bool isMergeTower = false;
    bool isSameTowerGround = false;
    bool isclickTowerGround = false;

    public event Action<TowerGroundData, TowerData> dropBuyTower;//
    event Action onBuyTowerDropped;//
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
    }
    private void Update()
    {
        //Debug.LogError(isclickTowerGround);
        //Debug.LogError("isSamaeTowerGround : " + isSameTowerGround);
        MouseButtonDown();
        ScreenToRayUseMouse();
        MouseButtonUp();
        if (towerPlaceManager.buyTowerObj != null)
        {
            MoveBuyTower(towerPlaceManager.buyTowerObj);
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
                if (towerGroundManager.detectedTowerGroundData != null)
                {
                    outMouseOnGround?.Invoke(towerGroundManager.detectedTowerGroundData);
                    isMouseOnGround = false;
                }
                towerGroundManager.detectedTowerGroundData = towerGround.towerGroundData;

                isFindGround = true;
                inMouseOnGround?.Invoke(towerGroundManager.detectedTowerGroundData);
                isMouseOnGround = true;
                break;
            }
        }
        if (isFindGround && towerGroundManager.detectedTowerGroundData.towerData != null) //여기서 팝업 생성
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
            if (towerGroundManager.detectedTowerGroundData != null)
            {
                outMouseOnGround?.Invoke(towerGroundManager.detectedTowerGroundData);
                isMouseOnGround = false;
            }
            towerGroundManager.detectedTowerGroundData = null;
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
        //상점
        if (Input.GetMouseButtonDown(0) && isBuingTower && isMouseOnGround)
        {
            dropBuyTower?.Invoke(towerGroundManager.detectedTowerGroundData, towerPlaceManager.buyTowerData);
            onBuyTowerDropped?.Invoke();
            isBuingTower = false;
        }
        if (Input.GetMouseButtonDown(0) && !isclickTowerGround)
        {
            isSameTowerGround = towerGroundManager.IsSameGroundSelected(clickTowerGround);
            towerGroundManager.SetCurrentSelectedGround(clickTowerGround);
            isclickTowerGround = true;
        }
        //필드 타워 선택
        if (Input.GetMouseButtonDown(0) && !isBuingTower && isMouseOnGround)
        {
            selectTowerData?.Invoke(towerGroundManager.detectedTowerGroundData.towerData);
            if (towerGroundManager.detectedTowerGroundData.towerData == null)
            {
                hasTwoSlectedTower = false;
            }
        }
        //선택된 타워가 두개가 있을 경우
        if (Input.GetMouseButtonDown(0) && !isBuingTower && isMouseOnGround && hasTwoSlectedTower && !isSameTowerGround)
        {
            //첫번째 선택된 타워와 두번째 선택된 타워가 같다면
            if (isMergeTower)
            {
                Debug.LogError("합체");
            }
            //첫번째 선택된 타워와 두번째 선택된 타워가 다르다면
            else
            {
                Debug.LogError("합체못함");
            }

        }
        //그라운드가 아닌 곳에서 클릭하면 초기화
        if (Input.GetMouseButtonDown(0) && !isMouseOnGround && towerGroundManager.detectedTowerGroundData == null)
        {
            clearData?.Invoke();
        }
    }
    void MouseButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isclickTowerGround = false;
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
    public void MoveBuyTower(GameObject obj)
    {
        Vector3 mousePosition = CurrentMousePos();
        obj.transform.position = mousePosition;
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
}