using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    public static MouseInteraction instance;
    TowerGroundManager towerGroundManager;
    TowerManager towerManager;

    public GameObject towerPopupPrefab;
    public RectTransform popupTransform;
    TowerManagerPopup towerPopup;

    public event Action<TowerGroundData> inMouseOnGround;
    public event Action<TowerGroundData> outMouseOnGround;

    public event Action<TowerGroundData> selectFirstTowerGround;
    public event Action<TowerGroundData> selectSecondTowerGround;
    public event Action onFieldTowerDrop;
    public event Action<bool> possibleMergeTower;

    public event Action<TowerGroundData, TowerData> dropBuyTower;
    public event Action onBuyTowerDropped;
    bool isBuingTower = false;

    public event Action openPopupInfo;
    public event Action closePopupInfo;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        towerGroundManager = TowerGroundManager.instance;
        towerManager = GameData.instance.towerManager;
        onBuyTowerDropped += towerManager.BuyTowerDropOnGround;
    }
    private void Update()
    {
        ScreenToRayUseMouse();
        MouseButtonDown();
        MouseButtonDrag();
        MouseButtonUP();
        if (towerManager.buyTowerObj != null)
        {
            MoveBuyTower(towerManager.buyTowerObj);
        }
    }
    void ScreenToRayUseMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        bool isFindGround = false;
        foreach (RaycastHit hit in hits)
        {
            if (!hit.collider.CompareTag("UI"))
            {
                //towerGroundManager.firstSelectedTowerGroundData = null;
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
        if (isFindGround && towerGroundManager.detectedTowerGroundData.towerData != null) //���⼭ �˾� ����
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
            }
            towerGroundManager.detectedTowerGroundData = null;
        }
    }
    void MouseButtonDown()
    {
        //ó�� �ʵ� Ÿ�� ����
        if (Input.GetMouseButtonDown(0) && string.IsNullOrEmpty(towerGroundManager.firstSelectedTowerGroundData.towerData.towerID) &&towerManager.buyTowerObj == null
            && towerGroundManager.detectedTowerGroundData != null           )
        {
            selectFirstTowerGround?.Invoke(towerGroundManager.detectedTowerGroundData);
            return;
        }
        //�ι�° �ʵ� Ÿ�� ����
        if (Input.GetMouseButtonDown(0) && towerGroundManager.firstSelectedTowerGroundData!= null && !string.IsNullOrEmpty(towerGroundManager.firstSelectedTowerGroundData.towerData.towerID)
            && towerGroundManager.detectedTowerGroundData != null && towerGroundManager.detectedTowerGroundData.towerData != null)
        {
            //ù��° ���õ� Ÿ���� �ι�° ���õ� Ÿ���� ���ٸ�
            if (towerGroundManager.firstSelectedTowerGroundData.towerData.towerID == towerGroundManager.detectedTowerGroundData.towerData.towerID)
            {
                towerGroundManager.UnregisterTowerData();
                Debug.LogError("������ ��ü");
            }
            //ù��° ���õ� Ÿ���� �ι�° ���õ� Ÿ���� �ٸ��ٸ�
            else
            {
                towerGroundManager.UnregisterTowerData();
                Debug.LogError("�ٸ��� ��ȯ");
                return;
            }
        }
    }
    void MouseButtonDrag()
    {
        if (Input.GetMouseButton(0))
        {
           // Debug.LogError("MouseButton : ���콺 �巡�� ��");
        }
    }

    void MouseButtonUP()
    {
        //����
        if (Input.GetMouseButtonUp(0) && towerManager.buyTowerObj != null && !isBuingTower)
        {
            dropBuyTower?.Invoke(towerGroundManager.detectedTowerGroundData, towerManager.buyTowerData);
            onBuyTowerDropped?.Invoke();
        }
            isBuingTower = false;
        //�ʵ�
        if (!Input.GetMouseButtonUp(0) && towerGroundManager.firstSelectedTowerGroundData != null)
        {

        }
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
            Debug.LogError("����!~!!~");
            return Vector3.zero;
        }
    }
}