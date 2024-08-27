using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerGround : MonoBehaviour 
{
    public TowerGroundData towerGroundData; //Ÿ�� �׶��尡 �����͸� ����ִ°� �³�
    List<Tower> hasTowers = new List<Tower>();
    public Tower currentTower;
    public string currentTowerID;
    public bool isHasTower { get; private set; }
    public void DropTower(TowerData data)
    {
        string towerID = data.towerID;
        ActorType type = data.type;
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
