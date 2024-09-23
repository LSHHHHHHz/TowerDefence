using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerGround : MonoBehaviour 
{
    public TowerGroundData towerGroundData;
    TowerGroundEffect towerGroundEffect;
    public Tower currentTower;
    public bool isHasTower { get; private set; }
    private void Awake()
    {
        towerGroundEffect = GetComponent<TowerGroundEffect>();
    }
    public void DropTower(TowerGround groundData, TowerData data)
    {       
        bool hasSameData = false;
        //데이터가 없는게 들어오면 비활성화
        if(groundData.towerGroundData.towerGroundNum == towerGroundData.towerGroundNum && data == null)
        {
            return;
        }
        //선택된 그라운드 아니면 return
        if (groundData.towerGroundData.towerGroundNum != towerGroundData.towerGroundNum || data.towerID == null)
        {
            return;
        }
        if (currentTower != null)
        {
            Destroy(currentTower.gameObject);
            currentTower = null;
        }
        string towerID = data.towerID;
        if (!hasSameData)
        {
            string path = GameManager.instance.gameEntityData.GetProfileDB(towerID).prefabPath;
            GameObject obj = Resources.Load<GameObject>(path);
            Tower tower = Instantiate(obj, transform).GetComponent<Tower>();
            currentTower = tower;
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
            if (ground.currentTower != null)
            {
                ground.currentTower.detectActorRange.ShowActorDetectRange(currentTower.towerStatus.attackRange);
            }
        }
    }
    public void OnExitGround(TowerGround ground)
    {
        if (ground != null && ground == this)
        {
            towerGroundEffect.ChangeGroundColorExitMouse();
            if (ground.currentTower != null)
            {
                ground.currentTower.detectActorRange.CloseActorDetectRange();
            }
        }
    }
}
