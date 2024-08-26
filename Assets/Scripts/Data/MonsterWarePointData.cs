//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using Unity.VisualScripting;
//using UnityEngine;

//[Serializable]
//public class MonsterWarePointData
//{
//    public List<List<Vector3>> monsterWarePointPos;

//    public MonsterWarePointData()
//    {
//        monsterWarePointPos = new List<List<Vector3>>();
//        InitializeData();
//    }

//    private void InitializeData()
//    {
//        List<Vector3> warePoint1 = new List<Vector3>
//        {
//            new Vector3(5.6f ,0, -7.8f),
//            new Vector3(5.6f,-3.3f,11.8f),
//            new Vector3(-42.3f,-3.3f,11.8f),
//            new Vector3(-42.3f,-1.1f,26)
//        };

//        List<Vector3> warePoint2 = new List<Vector3>
//        {
//            new Vector3(-42.3f,0,-7.8f),
//            new Vector3(-42.3f,-3.3f,11.8f),
//            new Vector3(5.6f,-3.3f,11.8f),
//            new Vector3(5.6f,-1.1f,26),
//        };

//        monsterWarePointPos.Add(warePoint1);
//        monsterWarePointPos.Add(warePoint2);
//    }
//    public List<Vector3> GetWarePointPos(string color)
//    {
//        switch (color)
//        {
//            case "Red":
//                return monsterWarePointPos[0];
//            case "Blue":
//                return monsterWarePointPos[1];
//            default
//                :
//                return null;
//        }
//    }
//}