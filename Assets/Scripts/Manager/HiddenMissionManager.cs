using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UI;
public class HiddenMissionManager : MonoBehaviour 
{
    ActorManager<Tower> actorManager;
    public HiddenMissionPopupUI hiddenMissionPopupUI;
    IReadOnlyList<Tower> towerList;
    Dictionary<int, List<string>> missionDic;
    bool[] isClearMission;

    public event Action onClearMission;

    private void Awake()
    {
        actorManager = ActorManager<Tower>.instnace;
        missionDic = new Dictionary<int, List<string>>();
        missionDic.Add(0, new List<string> { "nor11" });
        missionDic.Add(1, new List<string> { "nor12", "nor22", "nor32", "nor42" });

        isClearMission = new bool[missionDic.Count];
        EventManager.instance.onSetTower += CheckMissionTower;
        SetTowerList();
    }
    void SetTowerList()
    {
        towerList = actorManager.GetActors();
    }
    void CheckMissionTower()
    {
        List<string> list = new List<string>();     
        for (int i = 0; i < missionDic.Count; i++)
        {
            if((missionDic.ContainsKey(i)))
            {
                list = missionDic[i];
                bool[] isHasTowers = new bool[list.Count];
                for (int j = 0; j < list.Count; j++)
                {
                    for (int k = 0; k < towerList.Count; k++)
                    {
                        if (list[j] == towerList[k].actorId)
                        {
                            isHasTowers[j] = true;
                            break;
                        }
                        isHasTowers[j] = false;
                    }
                }
                bool allTowersFound = true;
                foreach (bool hasTower in isHasTowers)
                {
                    if (!hasTower)
                    {
                        allTowersFound = false;
                        break;
                    }
                }
                if (allTowersFound && !isClearMission[i])
                {
                    isClearMission[i] = true;
                    ClearHiddenMission(i);
                }
            }
        }
    }
    void ClearHiddenMission(int index)
    {
        HiddinMissionDB db = GameManager.instance.gameEntityData.GetHiddenDB(index);
        hiddenMissionPopupUI.gameObject.SetActive(true);
        hiddenMissionPopupUI.SetData(db);
    }
}
