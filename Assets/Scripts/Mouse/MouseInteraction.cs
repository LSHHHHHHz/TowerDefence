using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInteraction : MonoBehaviour
{
    public static MouseInteraction instance;

    public GameObject towerStatusPopupPrefab;
    public RectTransform towerStatusPopupTransform;
    TowerStatusPopup towerStatusPopup;

    GameObject dragObj;
    public TowerGround detectedTowerGround;
    public TowerGround firstClickTowerGround;
    public TowerGround secondSelecttowerGround;
    TowerData boughtShopTowerData;
    public event Action<TowerGround, TowerData> dropTowerOnGround;
    public event Action<TowerGround> inMouseOnGround;
    public event Action<TowerGround> outMouseOnGround;
    bool isBuingTower = false;//
    bool isMouseOnGround = false;
    bool isClickedGround = false;
    private void Awake()
    {
        instance = this;
        towerStatusPopup = Instantiate(towerStatusPopupPrefab, towerStatusPopupTransform).GetComponent<TowerStatusPopup>();
        towerStatusPopup.gameObject.SetActive(false);
    }
    private void Start()
    {
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
                }
                detectedTowerGround = null;//
                detectedTowerGround = towerGround;
                isFindGround = true;
                inMouseOnGround?.Invoke(detectedTowerGround);
                isMouseOnGround = true;
                break;
            }
        }
        if (isFindGround && detectedTowerGround.towerGroundData.towerData != null && detectedTowerGround.IsHasTower()) //여기서 팝업 생성
        {
            towerStatusPopup.gameObject.SetActive(true);
            towerStatusPopup.UpdatePopupData(detectedTowerGround.towerGroundData.towerData);
        }
        if (!isFindGround)
        {
            if (detectedTowerGround != null && detectedTowerGround.towerGroundData != null)
            {
                if(!isClickedGround)
                {
                    towerStatusPopup.gameObject.SetActive(false);
                }
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
        if (Input.GetMouseButtonDown(0))
        {
            // 구매한 타워가 없고 그라운드 내 마우스가 있으며 선택된 타워그라운드가 없을 때(처음 데이터 선택)
            if (!isBuingTower && isMouseOnGround && firstClickTowerGround == null)
            {
                isClickedGround = true;
                if (detectedTowerGround != null)
                {
                    firstClickTowerGround = detectedTowerGround;
                    if (firstClickTowerGround.towerGroundData.towerData == null)
                    {
                        firstClickTowerGround = null;
                    }
                }
                // Base타워 일 때
                if (!isBuingTower && isMouseOnGround && firstClickTowerGround != null
                    && firstClickTowerGround.towerGroundData != null
                    && firstClickTowerGround.towerGroundData.towerData != null
                    && firstClickTowerGround.towerGroundData.towerData.towerID == "nor01")
                {
                    Debug.Log("팝업 띄우고 바로 랜덤 돌릴 수 있게");
                    //isClickedGround = true;
                    firstClickTowerGround = null;
                }
            }
            // 구매한 타워가 없고 그라운드 내 마우스가 있으며 선택된 타워그라운드가 없을 때(기존 선택된 데이터가 있을 때)
            else if (!isBuingTower && isMouseOnGround && firstClickTowerGround != null)
            {
                isClickedGround = true;
                if (detectedTowerGround != null)
                {
                    secondSelecttowerGround = detectedTowerGround;
                    if (firstClickTowerGround.towerGroundData.towerData == null)
                    {
                        secondSelecttowerGround = null;
                    }
                }
                // 첫 번째 선택된 타워와 두 번째 선택된 타워가 다를 때
                if (!isBuingTower && isMouseOnGround && secondSelecttowerGround != null
                    && secondSelecttowerGround.towerGroundData != null
                    && secondSelecttowerGround.towerGroundData.towerData != null)
                {
                    if (firstClickTowerGround.towerGroundData.towerGroundNum != secondSelecttowerGround.towerGroundData.towerGroundNum)
                    {
                        if (firstClickTowerGround.towerGroundData.towerData.status.elementaProperties == secondSelecttowerGround.towerGroundData.towerData.status.elementaProperties &&
                            firstClickTowerGround.towerGroundData.towerData.status.level == secondSelecttowerGround.towerGroundData.towerData.status.level)
                        {
                            Debug.Log("타입과 레벨 같음 ! UI에서 합체 버튼 활성화");
                            dropTowerOnGround?.Invoke(secondSelecttowerGround,
                                GameManager.instance.gameEntityData.GetUpgradeTowerData(secondSelecttowerGround.towerGroundData.towerData));
                            dropTowerOnGround?.Invoke(firstClickTowerGround, null);
                            return;
                        }
                        else
                        {
                            Debug.Log("타입 또는 레벨이 다름 ! 꺼지게 삐삐 소리");
                        }
                        firstClickTowerGround = null;
                        secondSelecttowerGround = null;
                    }
                }
            }
            // 그라운드가 아닌 곳에서 클릭하면 초기화
            else if (!isBuingTower && !isMouseOnGround)
            {
                isClickedGround = false;
                firstClickTowerGround = null;
            }
            // 상점에서 타워를 드랍할 때
            else if (isBuingTower && isMouseOnGround)
            {
                dropTowerOnGround?.Invoke(detectedTowerGround, boughtShopTowerData);
                EventManager.instance.DropTowerForDraggableTowerClearData(); // DraggableTower에서 선택된 데이터 초기화
                boughtShopTowerData = null;
                isBuingTower = false;
            }
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