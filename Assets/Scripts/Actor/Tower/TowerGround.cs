using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerGround : MonoBehaviour 
{
    public TowerOnGroundData towerGroundData;
    List<Tower> hasTowers = new List<Tower>();
    public Tower currentTower;
    public string currentTowerID;
    public bool isHasTower { get; private set; }
    private void Start()
    {
        towerGroundData.setTowerData += DropTower;
        towerGroundData.resetTowerData += RemoveTower;
    }
    public void DropTower(string towerID, ActorType type)
    {
        foreach (Tower t in hasTowers)
        {
            t.gameObject.SetActive(false);
        }
        string path = GameManager.instance.gameEntityData.GetProfileDB(towerID).prefabPath;
        currentTowerID = towerID;
        GameObject obj = Resources.Load<GameObject>(path);
        Tower tower = Instantiate(obj, transform).GetComponent<Tower>();
        foreach(Tower t in hasTowers)
        {
            if(t == tower)
            {
                currentTower = t;
                currentTower.gameObject.SetActive(true);
                hasTowers.Add(currentTower);
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
}
