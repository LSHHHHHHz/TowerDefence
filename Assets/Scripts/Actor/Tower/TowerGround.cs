using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerGround : MonoBehaviour 
{
    public TowerOnGroundData towerGroundData;
    List<Tower> towers = new List<Tower>();
    public Tower currentTower;
    public bool isHasTower { get; private set; }
    private void Start()
    {
        towerGroundData.setTowerData += DropTower;
        towerGroundData.resetTowerData += RemoveTower;
    }
    public void DropTower(string towerID, ActorType type)
    {
        foreach (Tower t in towers)
        {
            t.gameObject.SetActive(false);
        }
        string path = GameManager.instance.gameEntityData.GetProfileData(towerID).prefabPath;
        GameObject obj = Resources.Load<GameObject>(path);
        Tower tower = Instantiate(obj, transform).GetComponent<Tower>();
        foreach(Tower t in towers)
        {
            if(t == tower)
            {
                currentTower = t;
                currentTower.gameObject.SetActive(true);
                towers.Add(currentTower);
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
