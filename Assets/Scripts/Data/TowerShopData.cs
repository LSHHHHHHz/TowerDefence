using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
[Serializable]
public class TowerShopData
{
    //리스트로 타워 이름 관리
    //관리한 리스트를 갖고 타워데이터에 접근
    public List<string> listTowerID;
    public TowerShopData()
    {
        listTowerID = new List<string>();
        Initilized();
    }
    void Initilized()
    {
        listTowerID.Add("nor11");
        listTowerID.Add("nor21");
        listTowerID.Add("nor31");
        listTowerID.Add("cham11");
        listTowerID.Add("cham21");
        listTowerID.Add("cham31");
    }
}