using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGround : MonoBehaviour 
{
    public bool isHasTower;
    Tower tower;
    private void OnEnable()
    {
        GameManager.instance.towerGroundEventManager.towerOnGround += DropTower;
        GameManager.instance.towerGroundEventManager.towerOutGround += RemoveTower;
    }
    private void OnDisable()
    {
        GameManager.instance.towerGroundEventManager.towerOnGround -= DropTower;
        GameManager.instance.towerGroundEventManager.towerOutGround -= RemoveTower;
    }
    void DropTower(Tower tower)
    {
        this.tower = tower;
        isHasTower = true;
    }
    void RemoveTower()
    {
        tower = null;
        isHasTower= false;
    }
    public bool ISHasTower()
    {
        return isHasTower;
    }
}
