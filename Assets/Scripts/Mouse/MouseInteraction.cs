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
        if (isFindGround && detectedTowerGround.towerGroundData.towerData != null && detectedTowerGround.IsHasTower()) //���⼭ �˾� ����
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
            // ������ Ÿ���� ���� �׶��� �� ���콺�� ������ ���õ� Ÿ���׶��尡 ���� ��(ó�� ������ ����)
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
                // BaseŸ�� �� ��
                if (!isBuingTower && isMouseOnGround && firstClickTowerGround != null
                    && firstClickTowerGround.towerGroundData != null
                    && firstClickTowerGround.towerGroundData.towerData != null
                    && firstClickTowerGround.towerGroundData.towerData.towerID == "nor01")
                {
                    Debug.Log("�˾� ���� �ٷ� ���� ���� �� �ְ�");
                    //isClickedGround = true;
                    firstClickTowerGround = null;
                }
            }
            // ������ Ÿ���� ���� �׶��� �� ���콺�� ������ ���õ� Ÿ���׶��尡 ���� ��(���� ���õ� �����Ͱ� ���� ��)
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
                // ù ��° ���õ� Ÿ���� �� ��° ���õ� Ÿ���� �ٸ� ��
                if (!isBuingTower && isMouseOnGround && secondSelecttowerGround != null
                    && secondSelecttowerGround.towerGroundData != null
                    && secondSelecttowerGround.towerGroundData.towerData != null)
                {
                    if (firstClickTowerGround.towerGroundData.towerGroundNum != secondSelecttowerGround.towerGroundData.towerGroundNum)
                    {
                        if (firstClickTowerGround.towerGroundData.towerData.status.elementaProperties == secondSelecttowerGround.towerGroundData.towerData.status.elementaProperties &&
                            firstClickTowerGround.towerGroundData.towerData.status.level == secondSelecttowerGround.towerGroundData.towerData.status.level)
                        {
                            Debug.Log("Ÿ�԰� ���� ���� ! UI���� ��ü ��ư Ȱ��ȭ");
                            dropTowerOnGround?.Invoke(secondSelecttowerGround,
                                GameManager.instance.gameEntityData.GetUpgradeTowerData(secondSelecttowerGround.towerGroundData.towerData));
                            dropTowerOnGround?.Invoke(firstClickTowerGround, null);
                            return;
                        }
                        else
                        {
                            Debug.Log("Ÿ�� �Ǵ� ������ �ٸ� ! ������ �߻� �Ҹ�");
                        }
                        firstClickTowerGround = null;
                        secondSelecttowerGround = null;
                    }
                }
            }
            // �׶��尡 �ƴ� ������ Ŭ���ϸ� �ʱ�ȭ
            else if (!isBuingTower && !isMouseOnGround)
            {
                isClickedGround = false;
                firstClickTowerGround = null;
            }
            // �������� Ÿ���� ����� ��
            else if (isBuingTower && isMouseOnGround)
            {
                dropTowerOnGround?.Invoke(detectedTowerGround, boughtShopTowerData);
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