using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerGround : MonoBehaviour 
{
    TowerOnGroundData towerGroundData;
    Tower tower;
    string installTowerID;
    ActorType type;
    public bool isHasTower;
    private void OnEnable()
    {
       // GameManager.instance.towerGroundEventManager.towerOnGround += DropTower;
        //GameManager.instance.towerGroundEventManager.towerOutGround += RemoveTower;
    }
    private void OnDisable()
    {
        //GameManager.instance.towerGroundEventManager.towerOnGround -= DropTower;
        //GameManager.instance.towerGroundEventManager.towerOutGround -= RemoveTower;
    }
    public void DropTower(string towerID, ActorType type)
    {
        this.installTowerID = towerID;
        this.type = type;
        string path = GameData.instance.towerData.GetTowerStatusData(towerGroundData.towerID, towerGroundData.type).Profile.prefabPath;
        GameObject obj = Resources.Load<GameObject>(path);
        tower = Instantiate(obj, transform).GetComponent<Tower>();
    }
    public void RemoveTower()
    {
        installTowerID = null;
    }
    public bool ISHasTower()
    {
        return isHasTower;
    }
}
