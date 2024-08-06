using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerGround : MonoBehaviour 
{
    public TowerOnGroundData towerGroundData;
    List<Tower> towers = new List<Tower>();
    public bool isHasTower;
    public void DropTower(string towerID, ActorType type)
    {
        foreach (Tower t in towers)
        {
            t.gameObject.SetActive(false);
        }
        string path = GameData.instance.towerData.GetTowerStatusData(towerID, type).Profile.prefabPath;
        GameObject obj = Resources.Load<GameObject>(path);
        Tower tower = Instantiate(obj, transform).GetComponent<Tower>();
        foreach(Tower t in towers)
        {
            if(t == tower)
            {
                t.gameObject.SetActive(true);
                towers.Add(t);
                break;
            }
        }
        isHasTower = true;
    }
    public void RemoveTower()
    {
        foreach (Tower t in towers)
        {
            t.gameObject.SetActive(false);
        }
        isHasTower = false;
    }
    public bool ISHasTower()
    {
        return isHasTower;
    }
}
