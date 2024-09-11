using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerGround : MonoBehaviour 
{
    public TowerGroundData towerGroundData;
    TowerGroundEffect towerGroundEffect;
    List<Tower> hasTowers;
    public Tower currentTower;
    public bool isHasTower { get; private set; }
    private void Awake()
    {
        hasTowers = new List<Tower>();
        towerGroundEffect = GetComponent<TowerGroundEffect>();
    }
    public void DropTower(TowerGround groundData, TowerData data)
    {       
        bool hasSameData = false;
        //데이터가 없는게 들어오면 비활성화
        if(groundData.towerGroundData.towerGroundNum == towerGroundData.towerGroundNum && data == null)
        {
            foreach (Tower t in hasTowers)
            {
                t.gameObject.SetActive(false);
            }
            isHasTower = false;
            return;
        }
        //선택된 그라운드 아니면 return
        if (groundData.towerGroundData.towerGroundNum != towerGroundData.towerGroundNum || data.towerID == null)
        {
            return;
        }
        //저장된 모든 Tower 비활성화
        foreach (Tower t in hasTowers)
        {
            t.gameObject.SetActive(false);
        }
        string towerID = data.towerID;
        foreach (Tower t in hasTowers)
        {
            if(t.towerData.towerID == towerID)
            {
                t.gameObject.SetActive(true);
                t.towerData = data;
                hasSameData = true;
            }
        }
        if (!hasSameData)
        {
            string path = GameManager.instance.gameEntityData.GetProfileDB(towerID).prefabPath;
            GameObject obj = Resources.Load<GameObject>(path);
            Tower tower = Instantiate(obj, transform).GetComponent<Tower>();
            currentTower = tower;
            hasTowers.Add(currentTower);
            currentTower.towerData = data;
            towerGroundData.towerData = data;
        }
        isHasTower = true;
    }
    public bool IsHasTower()
    {
        return isHasTower;
    }
    public void OnEnterGround(TowerGround ground)
    {
        if (ground != null && ground == this)
        {
            towerGroundEffect.ChangeGroundColorEnterMouse();
        }
    }
    public void OnExitGround(TowerGround ground)
    {
        if (ground != null && ground == this)
        {
            towerGroundEffect.ChangeGroundColorExitMouse();
        }
    }
}
