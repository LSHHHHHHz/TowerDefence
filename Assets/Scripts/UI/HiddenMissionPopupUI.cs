using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenMissionPopupUI : MonoBehaviour
{
    Player player;
    HiddinMissionDB hiddinMissionDB;
    RectTransform rect;
    [SerializeField] Text hiddenMissionNameText;
    [SerializeField] Text hiddenMissionRewardCoinText;

    Vector2 originPos;
    Vector2 centerPos;
    Vector2 targetPos;
    Vector2 centerTopRight;
    Vector2 centerBottomLeft; 

    private void Awake()
    {
        player = GameManager.instance.player;
        rect = GetComponent<RectTransform>();
        originPos = new Vector2(-1500, -130);
        targetPos = new Vector2(1500, 130);
        centerPos = new Vector2(0, 0);
        centerTopRight = new Vector2(128, 11); 
        centerBottomLeft = new Vector2(-128, -11); 
        rect.anchoredPosition = originPos;
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        rect.anchoredPosition = originPos;
    }
    public void SetData(HiddinMissionDB db)
    {
        hiddinMissionDB = db;
        hiddenMissionNameText.text = db.missionName;
        hiddenMissionRewardCoinText.text = db.rewardCoin.ToString();
        StartCoroutine(MovePopup());
    }
    IEnumerator MovePopup()
    {
        yield return MoveToPosition(rect, centerTopRight, 0.3f);
        yield return MoveToPosition(rect, centerPos, 0.2f);
        yield return new WaitForSeconds(2);
        yield return MoveToPosition(rect, centerBottomLeft, 0.2f);
        yield return MoveToPosition(rect, targetPos, 0.1f);
        gameObject.SetActive(false);

        player.GetCoin(hiddinMissionDB.rewardCoin);
    }
    IEnumerator MoveToPosition(RectTransform rectTransform, Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = targetPosition;
    }
}
