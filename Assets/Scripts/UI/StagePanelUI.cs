using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StagePanelUI : MonoBehaviour
{
    StageManager stageManager;
    [SerializeField] Text stageNumberText;
    [SerializeField] Image normarMonsterImage;
    [SerializeField] Text normarMonsterHp;
    [SerializeField] Text normarMonsterRewardCoin;
    [SerializeField] Image bossMonsterImage;
    [SerializeField] Text bossMonsterHp;
    [SerializeField] Text bossMonsterRewardCoin;
    [SerializeField] Image remainMonsterImage;
    [SerializeField] Text remainmonsterRemainCount;
    private void Awake()
    {
        stageManager = GameManager.instance.stageManager;
        stageManager.onInitializedStage += UpdateStageInfo;
        stageManager.onUpdateCurrentMonsterTypeIconPath += UpdateRemainMonsterImage;
        stageManager.onUpdateCurrentMonsterCount += UpdateRemainMonsterCount;
    }
    void UpdateStageInfo(int num, string normarMonsterIconPath, int normarMonsterHP, int normarMonsterRewardCoin,
                                    string bossMonsterIconPath, int bossMonsterHP, int bossMonsterRewardCoin)
    {
        stageNumberText.text = "Stage : " + num.ToString();

        normarMonsterImage.sprite = Resources.Load<Sprite>(normarMonsterIconPath);
        this.normarMonsterHp.text = "Ã¼·Â : " + normarMonsterHP.ToString();
        this.normarMonsterRewardCoin.text ="ÄÚÀÎ È¹µæ·® : " + normarMonsterRewardCoin.ToString();

        bossMonsterImage.sprite = Resources.Load<Sprite>(bossMonsterIconPath);
        this.bossMonsterHp.text = "Ã¼·Â : " +bossMonsterHP.ToString();
        this.bossMonsterRewardCoin.text = "ÄÚÀÎ È¹µæ·® : " + bossMonsterRewardCoin.ToString();

    }
    void UpdateRemainMonsterImage(string path)
    {
        remainMonsterImage.sprite = Resources.Load<Sprite>(path);
    }
    void UpdateRemainMonsterCount(int remainMonsterCount)
    {
        remainmonsterRemainCount.text = "¸ó½ºÅÍ ¼ö : " + remainMonsterCount.ToString();
    }
}