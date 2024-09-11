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
        if(groundData.towerGroundData.towerGroundNum != towerGroundData.towerGroundNum || data.towerID == null)
        {
            return;
        }
        string towerID = data.towerID;
        ActorType type = data.type;        
        foreach (Tower t in hasTowers)
        {
            t.gameObject.SetActive(false);
        }
        string path = GameManager.instance.gameEntityData.GetProfileDB(towerID).prefabPath;
        GameObject obj = Resources.Load<GameObject>(path);
        Tower tower = Instantiate(obj, transform).GetComponent<Tower>();
        currentTower = tower;
        if (!hasTowers.Contains(currentTower))
        {
            hasTowers.Add(currentTower);
        }
        foreach (Tower t in hasTowers)
        {
            if(t == tower)
            {
                currentTower = t;
                currentTower.gameObject.SetActive(true);
                currentTower.towerData = data;
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
