using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
[Serializable]
public class TowerShopData
{
    //����Ʈ�� Ÿ�� �̸� ����
    //������ ����Ʈ�� ���� Ÿ�������Ϳ� ����
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