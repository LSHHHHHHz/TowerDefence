using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageMonsterInfoUI : MonoBehaviour 
{
    [SerializeField] Image monsterimage;
    [SerializeField] Text monstername;
    [SerializeField] Text monsterRewardCoin;
    [SerializeField] Text monsterHp;
    [SerializeField] ActorType monsterType;

    public void SetMonsterData(ProfileDB profileDB, MonsterStatusDB statusDB)
    {
        SetMonsterInfoUI(profileDB.prefabPath, profileDB.name, statusDB.coin, statusDB.hp);
    }
    private void SetMonsterInfoUI(string path, string name, int coin, int hp)
    {
        monsterimage.sprite = Resources.Load<Sprite>(path);
        monstername.text = name;
        monsterRewardCoin.text = "골드 보상 : " + coin.ToString();
        monsterHp.text = "체력 : " + hp.ToString();
    }
}
