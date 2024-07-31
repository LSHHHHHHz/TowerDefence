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
    [SerializeField] Text monsterRewardExp;
    [SerializeField] Text monsterHp;
    [SerializeField] ActorType monsterType;

    public void SetMonsterData(SetMonsterDatas data)
    {
        SetMonsterInfoUI(data.Profile.iconPath,data.Profile.actorName,data.Reward.rewardCoin,data.Reward.rewardExp,data.Status.maxHP);
    }
    private void SetMonsterInfoUI(string path, string name, int coin, int exp, int hp)
    {
        monsterimage.sprite = Resources.Load<Sprite>(path);
        monstername.text = name;
        monsterRewardCoin.text = "��� ���� : " + coin.ToString();
        monsterRewardExp.text = "����ġ ���� : " + exp.ToString();
        monsterHp.text = "ü�� : " + hp.ToString();
    }
}
