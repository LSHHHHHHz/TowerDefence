using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInteraction : MonoBehaviour
{
    public static MouseInteraction instance;

    public RectTransform towerStatusPopupTransform;
    public GameObject towerStatusPopupPrefab;
    TowerStatusPopup towerStatusPopup;

    GameObject dragObj;
    public TowerGround detectedTowerGround;
    public TowerGround firstClickTowerGround;
    public TowerGround secondSelecttowerGround;
    public TowerGround detectedBaseTowerGround;
    TowerData boughtShopTowerData;
    public event Action<TowerGround, TowerData> onDropTowerOnGround;
    public event Action<TowerGround> inMouseOnGround;
    public event Action<TowerGround> outMouseOnGround;
    public event Action onActiveMouseEffect;

    bool isBuingTower = false;
    bool isMouseOnGround = false;
    bool isClickedGround = false;
    private Vector3 originalPosition;

    private void Awake()
    {
        instance = this;
        towerStatusPopup = Instantiate(towerStatusPopupPrefab, towerStatusPopupTransform).GetComponent<TowerStatusPopup>();
        towerStatusPopup.gameObject.SetActive(false);
    }
    private void Start()
    {
        EventManager.instance.onBuyShopTower += BuingTower;
        EventManager.instance.onClickUpgradeButton += UpgradeTower;
    }
    private void Update()
    {
        MouseButtonDown();
        ScreenToRayUseMouse();
        MouseButtonUp();
        if (isBuingTower || (dragObj != null && firstClickTowerGround != null)) // 타워 구매 중이거나 드래그 중일 때
        {
            DragTowerObj(dragObj, boughtShopTowerData);
        }
    }
    void MouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerInStatusUI())
            {
                return;
            }
            // 기존 배치된 타워 클릭 시 드래그 가능하게 처리
            if (!isBuingTower && isMouseOnGround && detectedTowerGround != null)
            {
                isClickedGround = true;
                firstClickTowerGround = detectedTowerGround;
                detectedBaseTowerGround = detectedTowerGround;
                if (firstClickTowerGround.towerGroundData.towerData != null && firstClickTowerGround.towerGroundData.towerData.towerID != "nor01")
                {
                    dragObj = firstClickTowerGround.currentTower.gameObject; // 타워 오브젝트 가져오기
                    originalPosition = dragObj.transform.position; // 드래그 전 원래 위치 저장
                }
            }
            // 구매한 타워가 없고 그라운드 내 마우스가 있으며 선택된 타워그라운드가 없을 때(처음 데이터 선택)
            else if (!isBuingTower && isMouseOnGround && firstClickTowerGround == null)
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
                    onDropTowerOnGround?.Invoke(firstClickTowerGround,
                               GameManager.instance.gameEntityData.GetUpgradeTowerData(firstClickTowerGround.towerGroundData.towerData));
                    //isClickedGround = true;
                    EventManager.instance.ActiveAttack();
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
                            onDropTowerOnGround?.Invoke(secondSelecttowerGround,
                                GameManager.instance.gameEntityData.GetUpgradeTowerData(secondSelecttowerGround.towerGroundData.towerData));
                            onDropTowerOnGround?.Invoke(firstClickTowerGround, null);
                            EventManager.instance.ActiveAttack();
                            return;
                        }
                        else
                        {
                            Debug.Log("타입 또는 레벨이 다름 ! ");
                            towerStatusPopup.gameObject.SetActive(false);
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
                towerStatusPopup.gameObject.SetActive(false);
            }
            // 상점에서 타워를 드랍할 때
            else if (isBuingTower && isMouseOnGround)
            {
                onDropTowerOnGround?.Invoke(detectedTowerGround, boughtShopTowerData);
                EventManager.instance.ActiveAttack(); // DraggableTower에서 선택된 데이터 초기화
                boughtShopTowerData = null;
                isBuingTower = false;
            }
        }
    }
    void MouseButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (IsPointerInStatusUI())
            {
                return;
            }
            if (dragObj != null && detectedTowerGround != null)
            {
                if (firstClickTowerGround == null)
                {
                    return;
                }
                if (detectedTowerGround != firstClickTowerGround) // 다른 타워그라운드로 드랍했을 때
                {
                    // 타워 레벨과 프로퍼티 체크 후 업그레이드 시도
                    if (detectedTowerGround.towerGroundData != null && firstClickTowerGround.towerGroundData != null
                        && detectedTowerGround.towerGroundData.towerData != null
                        && firstClickTowerGround.towerGroundData.towerData != null
                        && detectedTowerGround.towerGroundData.towerData.status.elementaProperties == firstClickTowerGround.towerGroundData.towerData.status.elementaProperties
                        && detectedTowerGround.towerGroundData.towerData.status.level == firstClickTowerGround.towerGroundData.towerData.status.level)
                    {
                        Debug.Log("타워 업그레이드");
                        onDropTowerOnGround?.Invoke(detectedTowerGround,
                            GameManager.instance.gameEntityData.GetUpgradeTowerData(detectedTowerGround.towerGroundData.towerData));
                        Destroy(firstClickTowerGround.currentTower.gameObject);
                    }
                    else
                    {
                        Debug.Log("타워 업그레이드 불가");
                        dragObj.transform.position = originalPosition; // 원래 위치로 돌아감
                    }
                }
                else
                {
                    dragObj.transform.position = originalPosition; // 원래 위치로 돌아감
                }
                EventManager.instance.ActiveAttack();
                dragObj = null;
                firstClickTowerGround = null;
                secondSelecttowerGround = null;
            }
            onActiveMouseEffect?.Invoke();
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
            return Vector3.zero;
        }
    }
    public void BuingTower()
    {
        isBuingTower = true;
    }
    void ScreenToRayUseMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        bool isFindGround = false;
        foreach (RaycastHit hit in hits)
        {
            if (IsPointerInStatusUI())
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
                detectedTowerGround = null;
                detectedTowerGround = towerGround;
                isFindGround = true;
                inMouseOnGround?.Invoke(detectedTowerGround);
                isMouseOnGround = true;
                break;
            }
        }
        if (isFindGround && detectedTowerGround.towerGroundData.towerData != null && detectedTowerGround.IsHasTower())
        {
            towerStatusPopup.gameObject.SetActive(true);
            towerStatusPopup.UpdatePopupData(detectedTowerGround.towerGroundData.towerData);
        }
        if (!isFindGround)
        {
            if (detectedTowerGround != null && detectedTowerGround.towerGroundData != null)
            {
                if (!isClickedGround)
                {
                    towerStatusPopup.gameObject.SetActive(false);
                }
                outMouseOnGround?.Invoke(detectedTowerGround);
                isMouseOnGround = false;
            }
            detectedTowerGround = null;
        }
        if (detectedTowerGround != null && detectedTowerGround.currentTower == null && !isClickedGround)
        {
            towerStatusPopup.gameObject.SetActive(false);
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
    bool IsPointerInStatusUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.CompareTag("StatusUI"))
            {
                return true;
            }
        }
        return false;
    }
    public void UpgradeTower()
    {
        TowerData td = null;
        onDropTowerOnGround?.Invoke(detectedBaseTowerGround,
                           GameManager.instance.gameEntityData.GetUpgradeTowerData(detectedBaseTowerGround.towerGroundData.towerData));
        td = detectedBaseTowerGround.towerGroundData.towerData;
        towerStatusPopup.UpdatePopupData(GameManager.instance.gameEntityData.GetUpgradeTowerData(td));
    }
}
