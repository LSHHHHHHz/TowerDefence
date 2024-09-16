using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    StageManager stageManager;
    Player player;
    [SerializeField] Image playerDeathFadeImage;
    float playerDeathFadeImageElapsedTime = 0f;
    float playerDeathFadeImageDuration = 5f;

    [SerializeField] RectTransform restartTextRectTransform;
    Vector2 restartTextOriginPos = new Vector2(0, 650);
    Vector2 restartTextTargetPos = new Vector2(0, 80);

    [SerializeField] Button restartButton;
    int requiredDiaForRestart = 1000;
    bool isRestartText = false;
    private void Awake()
    {
        stageManager = GameManager.instance.stageManager;
        player = GameManager.instance.player;
    }
    private void Start()
    {
        restartTextRectTransform.anchoredPosition = restartTextOriginPos;
        restartButton.gameObject.SetActive(false);
        restartButton.onClick.AddListener(() =>
        {
            if (player.GetPlayerHasdDia() - requiredDiaForRestart >= 0)
            {
                player.SpendDia(requiredDiaForRestart);
                ResetGameoverContents();
                stageManager.ReStartStage();
            }
            else
            {
                Debug.Log("다이아 없어요 결제해주세요");
            }
        });

        playerDeathFadeImage.gameObject.SetActive(false);
    }
    public void DeathPlayer()
    {
        stageManager.PossibleStartStage(false);
        playerDeathFadeImage.gameObject.SetActive(true);
        StartCoroutine(StartDeathPlayer());
    }
    IEnumerator StartDeathPlayer()
    {
        playerDeathFadeImage.enabled = true;
        float alpha = 0;
        float elapsedTime = playerDeathFadeImageElapsedTime;
        while (elapsedTime < playerDeathFadeImageDuration)
        {
            elapsedTime += Time.deltaTime;
            alpha = Mathf.Lerp(alpha, 0.8f, elapsedTime / playerDeathFadeImageDuration);
            playerDeathFadeImage.color = new Color(playerDeathFadeImage.color.r, playerDeathFadeImage.color.g, playerDeathFadeImage.color.b, alpha);
            if (elapsedTime > 0.5f && !isRestartText)
            {
                ShowRestartText();
                isRestartText = true;
            }
            yield return null;
        }
        playerDeathFadeImage.color = new Color(playerDeathFadeImage.color.r, playerDeathFadeImage.color.g, playerDeathFadeImage.color.b, 0.8f);
    }
    void ShowRestartText()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(restartTextRectTransform.DOAnchorPos(new Vector2(restartTextTargetPos.x, restartTextTargetPos.y - 20), 0.2f));
        sequence.Append(restartTextRectTransform.DOAnchorPos(new Vector2(restartTextTargetPos.x, restartTextTargetPos.y + 10), 0.2f));
        sequence.Append(restartTextRectTransform.DOAnchorPos(restartTextTargetPos, 0.2f));

        sequence.AppendCallback(() => restartButton.gameObject.SetActive(true));
    }
    void ResetGameoverContents()
    {
        stageManager.PossibleStartStage(true);
        GameManager.instance.player.GetHP(30);
        //ActorManager<Monster>.instnace.ClearAllActor(); 이거 안됨
        restartButton.gameObject.SetActive(false);
        isRestartText = false;
        restartTextRectTransform.anchoredPosition = restartTextOriginPos;
        playerDeathFadeImage.gameObject.SetActive(false);
        StopAllCoroutines();
    }
}