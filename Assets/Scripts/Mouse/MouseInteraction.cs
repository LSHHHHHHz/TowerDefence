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
    public TowerGround clickTowerGround;
    TowerData boughtShopTowerData;
    public event Action<TowerGround, TowerData> dropBuyTowerOnGround;
    public event Action<TowerGround> inMouseOnGround;
    public event Action<TowerGround> outMouseOnGround;
    bool isBuingTower = false;//
    bool isMouseOnGround = false;
    private void Awake()
    {
        instance = this;
        towerStatusPopup = Instantiate(towerStatusPopupPrefab,towerStatusPopupTransform).GetComponent<TowerStatusPopup>();
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
        if (isFindGround && detectedTowerGround.towerGroundData.towerData != null) //���⼭ �˾� ����
        {
            if (towerStatusPopup == null)
            {
                //towerStatusPopup = Instantiate(towerPopupPrefab, popupTransform).GetComponent<TowerManagerPopup>();
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
        if (Input.GetMouseButtonDown(0))
        {
            TowerGround secondSelecttowerGround = null;
            // ������ Ÿ���� ���� �׶��� �� ���콺�� ������ ���õ� Ÿ���׶��尡 ���� ��(ó�� ������ ����)
            if (!isBuingTower && isMouseOnGround && clickTowerGround == null)
            {
                if (detectedTowerGround != null)
                {
                    clickTowerGround = detectedTowerGround;
                    if (clickTowerGround.towerGroundData.towerData == null)
                    {
                        clickTowerGround = null;
                    }
                }
                // BaseŸ�� �� ��
                if (!isBuingTower && isMouseOnGround && clickTowerGround != null
                    && clickTowerGround.towerGroundData != null
                    && clickTowerGround.towerGroundData.towerData != null
                    && clickTowerGround.towerGroundData.towerData.towerID == "nor01")
                {
                    Debug.Log("�˾� ���� �ٷ� ���� ���� �� �ְ�");
                    clickTowerGround = null;
                }
            }
            // ������ Ÿ���� ���� �׶��� �� ���콺�� ������ ���õ� Ÿ���׶��尡 ���� ��(���� ���õ� �����Ͱ� ���� ��)
            else if (!isBuingTower && isMouseOnGround && clickTowerGround != null)
            {
                if (detectedTowerGround != null)
                {
                    secondSelecttowerGround = detectedTowerGround;
                    if (clickTowerGround.towerGroundData.towerData == null)
                    {
                        secondSelecttowerGround = null;
                    }
                }                
                // ù ��° ���õ� Ÿ���� �� ��° ���õ� Ÿ���� �ٸ� ��
                if (!isBuingTower && isMouseOnGround && secondSelecttowerGround != null
                    && secondSelecttowerGround.towerGroundData != null
                    && secondSelecttowerGround.towerGroundData.towerData != null)
                {
                    if (clickTowerGround.towerGroundData.towerGroundNum != secondSelecttowerGround.towerGroundData.towerGroundNum)
                    {
                        if (clickTowerGround.towerGroundData.towerData.status.elementaProperties == secondSelecttowerGround.towerGroundData.towerData.status.elementaProperties &&
                            clickTowerGround.towerGroundData.towerData.status.level == secondSelecttowerGround.towerGroundData.towerData.status.level)
                        {
                            Debug.Log("Ÿ�԰� ���� ���� ! UI���� ��ü ��ư Ȱ��ȭ");
                        }
                        else
                        {
                            Debug.Log("Ÿ�� �Ǵ� ������ �ٸ� ! ������");
                        }
                        clickTowerGround = null;
                    }
                }
            }            
            // �׶��尡 �ƴ� ������ Ŭ���ϸ� �ʱ�ȭ
            else if (!isBuingTower && !isMouseOnGround)
            {
                clickTowerGround = null;
            }
            // �������� Ÿ���� ����� ��
            else if (isBuingTower && isMouseOnGround)
            {
                dropBuyTowerOnGround?.Invoke(detectedTowerGround, boughtShopTowerData);
                EventManager.instance.DropTowerForDraggableTowerClearData(); // DraggableTower���� ���õ� ������ �ʱ�ȭ
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
            Debug.LogError("����!~!!~");
            return Vector3.zero;
        }
    }
    public void BuingTower()
    {
        isBuingTower = true;
    }
}