using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerGround : MonoBehaviour 
{
    public TowerGroundData towerGroundData; 
    TowerGroundEffect towerGroundEffect;
    [SerializeField]List<Tower> hasTowers = new List<Tower>();
    public Tower currentTower;
    public bool isHasTower { get; private set; }
    private void Awake()
    {
        towerGroundEffect = GetComponent<TowerGroundEffect>();
    }
    public void DropTower(TowerGroundData groundData, TowerData data)
    {
        if(groundData != towerGroundData || data.towerID == null)
        {
            return;
        }
        string towerID = data.towerID;
        ActorType type = data.type;
        if (!hasTowers.Contains(currentTower) && currentTower != null)
        {
            hasTowers.Add(currentTower);
        }
        foreach (Tower t in hasTowers)
        {
            t.gameObject.SetActive(false);
        }
        string path = GameManager.instance.gameEntityData.GetProfileDB(towerID).prefabPath;
        GameObject obj = Resources.Load<GameObject>(path);
        Tower tower = Instantiate(obj, transform).GetComponent<Tower>();
        foreach(Tower t in hasTowers)
        {
            if(t == tower)
            {
                currentTower = t;
                currentTower.gameObject.SetActive(true);
                currentTower.towerData = data;
                towerGroundData = groundData;
                towerGroundData.towerData = data;
                break;
            }
        }
        isHasTower = true;
    }
    public void RemoveTower()
    {
        currentTower?.gameObject.SetActive(false);
        isHasTower = false;
    }
    public bool ISHasTower()
    {
        return isHasTower;
    }
    public void OnEnterGround(TowerGroundData data)
    {
        if (data != null && data == towerGroundData)
        {
            towerGroundEffect.ChangeGroundColorEnterMouse();
        }
    }
    public void OnExitGround(TowerGroundData data)
    {
        if (data != null && data == towerGroundData)
        {
            towerGroundEffect.ChangeGroundColorExitMouse();
        }
    }
}
