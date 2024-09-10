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
        if (isFindGround && detectedTowerGround.towerGroundData.towerData != null) //���⼭ �˾� ����
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
        //�ʵ� ����(������ Ÿ���� ���� �׶��� �� ���콺�� ������ ���õ� Ÿ���׶��尡 ���� ��)
        //�ʵ� Ÿ�� ����
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
            Debug.LogError("�˾� ���� �ٷ� ���� ���� �� �ְ�");
            clickTowerGround = null;
        }
        //�ʵ� ����(������ Ÿ���� ���� �׶��� �� ���콺�� ������ ���õ� Ÿ���׶��尡 ���� ��)

        //ù��° ���õ� Ÿ���� �ι�° ���õ� Ÿ���� ���ٸ�

        //ù��° ���õ� Ÿ���� �ι�° ���õ� Ÿ���� �ٸ��ٸ�
        if (Input.GetMouseButtonDown(0) && !isBuingTower && isMouseOnGround && clickTowerGround != null && clickTowerGround.towerGroundData != null && clickTowerGround.towerGroundData.towerData != null)
        {
            if (clickTowerGround.towerGroundData.towerGroundNum == detectedTowerGround.towerGroundData.towerGroundNum)
            {
                return;
            }
            else
            {
                //�Ӽ�Ÿ�԰� ������ ���ٸ�
                if (clickTowerGround.towerGroundData.towerData.status.elementaProperties == detectedTowerGround.towerGroundData.towerData.status.elementaProperties &&
                    clickTowerGround.towerGroundData.towerData.status.level == detectedTowerGround.towerGroundData.towerData.status.level)
                {
                    Debug.LogError("Ÿ�԰� ���� ���� ! UI���� ��ü ��ư Ȱ��ȭ");
                    clickTowerGround = null;
                }
                else
                {
                    Debug.LogError("Ÿ�� �Ǵ� ������ �ٸ� ! ������");
                    clickTowerGround = null;
                    return;
                }
            }
        }
        //�׶��尡 �ƴ� ������ Ŭ���ϸ� �ʱ�ȭ
        if (Input.GetMouseButtonDown(0) && !isBuingTower && !isMouseOnGround)
        {
            clickTowerGround = null;
        }

        //���� ���� Ÿ�� ���
        if (Input.GetMouseButtonDown(0) && isBuingTower && isMouseOnGround)
        {
            dropBuyTowerOnGround?.Invoke(detectedTowerGround, boughtShopTowerData);
            EventManager.instance.DropTowerForDraggableTowerClearData(); // DraggableTower���� ���õ� ������ �ʱ�ȭ
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
            Debug.LogError("����̳� ��� ������ ����");
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