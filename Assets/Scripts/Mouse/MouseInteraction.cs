using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInteraction : MonoBehaviour
{
    public static MouseInteraction instance;
    public RectTransform towerStatusPopupTransform;
    public GameObject towerStatusPopupPrefab;
    private TowerStatusPopup towerStatusPopup;
    private GameObject dragObj;
    public TowerGround detectedTowerGround;
    public TowerGround firstClickTowerGround;
    public TowerGround secondSelectTowerGround;
    public TowerGround detectedBaseTowerGround;
    private TowerData boughtShopTowerData;
    public event Action<TowerGround, TowerData> onDropTowerOnGround;
    public event Action<TowerGround> inMouseOnGround;
    public event Action<TowerGround> outMouseOnGround;
    public event Action onActiveMouseEffect;
    private bool isBuingTower = false;
    private bool isMouseOnGround = false;
    private bool isClickedGround = false;
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
        if (isBuingTower || (dragObj != null && firstClickTowerGround != null))
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
            if (!isBuingTower && isMouseOnGround && detectedTowerGround != null)
            {
                isClickedGround = true;
                firstClickTowerGround = detectedTowerGround;
                detectedBaseTowerGround = detectedTowerGround;
                if (firstClickTowerGround.towerGroundData.towerData != null && firstClickTowerGround.towerGroundData.towerData.towerID != "nor01")
                {
                    dragObj = firstClickTowerGround.currentTower.gameObject;
                    originalPosition = dragObj.transform.position;
                }
            }
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
                if (firstClickTowerGround != null && firstClickTowerGround.towerGroundData != null && firstClickTowerGround.towerGroundData.towerData != null && firstClickTowerGround.towerGroundData.towerData.towerID == "nor01")
                {
                    onDropTowerOnGround?.Invoke(firstClickTowerGround, GameManager.instance.gameEntityData.GetUpgradeTowerData(firstClickTowerGround.towerGroundData.towerData));
                    EventManager.instance.ActiveAttack();
                    firstClickTowerGround = null;
                }
            }
            else if (!isBuingTower && isMouseOnGround && firstClickTowerGround != null)
            {
                isClickedGround = true;
                if (detectedTowerGround != null)
                {
                    secondSelectTowerGround = detectedTowerGround;
                    if (firstClickTowerGround.towerGroundData.towerData == null)
                    {
                        secondSelectTowerGround = null;
                    }
                }
                if (secondSelectTowerGround != null && secondSelectTowerGround.towerGroundData != null && secondSelectTowerGround.towerGroundData.towerData != null)
                {
                    if (firstClickTowerGround.towerGroundData.towerGroundNum != secondSelectTowerGround.towerGroundData.towerGroundNum)
                    {
                        if (firstClickTowerGround.towerGroundData.towerData.status.elementaProperties == secondSelectTowerGround.towerGroundData.towerData.status.elementaProperties &&
                            firstClickTowerGround.towerGroundData.towerData.status.level == secondSelectTowerGround.towerGroundData.towerData.status.level)
                        {
                            onDropTowerOnGround?.Invoke(secondSelectTowerGround, GameManager.instance.gameEntityData.GetUpgradeTowerData(secondSelectTowerGround.towerGroundData.towerData));
                            onDropTowerOnGround?.Invoke(firstClickTowerGround, null);
                            EventManager.instance.ActiveAttack();
                            return;
                        }
                        firstClickTowerGround = null;
                        secondSelectTowerGround = null;
                    }
                }
            }
            else if (!isBuingTower && !isMouseOnGround)
            {
                isClickedGround = false;
                firstClickTowerGround = null;
                towerStatusPopup.gameObject.SetActive(false);
            }
            else if (isBuingTower && isMouseOnGround)
            {
                onDropTowerOnGround?.Invoke(detectedTowerGround, boughtShopTowerData);
                EventManager.instance.ActiveAttack();
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
                if (detectedTowerGround != firstClickTowerGround)
                {
                    if (detectedTowerGround.towerGroundData != null && firstClickTowerGround.towerGroundData != null
                        && detectedTowerGround.towerGroundData.towerData != null && firstClickTowerGround.towerGroundData.towerData != null
                        && detectedTowerGround.towerGroundData.towerData.status.elementaProperties == firstClickTowerGround.towerGroundData.towerData.status.elementaProperties
                        && detectedTowerGround.towerGroundData.towerData.status.level == firstClickTowerGround.towerGroundData.towerData.status.level)
                    {
                        onDropTowerOnGround?.Invoke(detectedTowerGround, GameManager.instance.gameEntityData.GetUpgradeTowerData(detectedTowerGround.towerGroundData.towerData));
                        GameObject upgradeEffect = PoolManager.instance.GetObjectFromPool("Prefabs/Tower/TowerWaeponEffect/UpgradeEffect");
                        upgradeEffect.transform.position = detectedTowerGround.transform.position + new Vector3(0, 1, 0);
                        Destroy(firstClickTowerGround.currentTower.gameObject);
                    }
                    else
                    {
                        dragObj.transform.position = originalPosition;
                    }
                }
                else
                {
                    dragObj.transform.position = originalPosition;
                }
                EventManager.instance.ActiveAttack();
                dragObj = null;
                firstClickTowerGround = null;
                secondSelectTowerGround = null;
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
        onDropTowerOnGround?.Invoke(detectedBaseTowerGround, GameManager.instance.gameEntityData.GetUpgradeTowerData(detectedBaseTowerGround.towerGroundData.towerData));
        td = detectedBaseTowerGround.towerGroundData.towerData;
        towerStatusPopup.UpdatePopupData(td);
        EventManager.instance.ActiveAttack();
        GameObject upgradeEffect = PoolManager.instance.GetObjectFromPool("Prefabs/Tower/TowerWaeponEffect/UpgradeEffect");
        upgradeEffect.transform.position = detectedBaseTowerGround.transform.position + new Vector3(0, 1, 0);
    }
}
