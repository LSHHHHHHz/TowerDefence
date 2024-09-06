using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRewardCoin : MonoBehaviour
{
    float posOffset = 1;
    float scaleOffset = 0.3f;
    float durationPos = 0.3f;
    float durationScale = 0.2f;
    Vector3 originPos;
    Vector3 originScale;
    private void Awake()
    {
        originPos = transform.position;
        originScale = transform.localScale;
    }
    private void OnEnable()
    {
        transform.position = originPos;
        transform.localScale = originScale;
        StartCoroutine(HandleCoinPos());
        StartCoroutine(HandleCoinScale());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator HandleCoinPos()
    {
        float elapsedTime = 0f;
        Vector3 startPos = originPos;
        Vector3 targetPos = startPos + new Vector3(0, posOffset, 0);

        while (elapsedTime < durationPos)
        {
            float t = elapsedTime / durationPos;
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
    }
    IEnumerator HandleCoinScale()
    {
        float elapsedTime = 0;
        Vector3 startScale = originScale;
        Vector3 targetScale = originScale - new Vector3(scaleOffset, scaleOffset, scaleOffset);
        while (elapsedTime < durationScale)
        {
            if (elapsedTime > 0.1f)
            {
                float t = elapsedTime / durationScale;
                transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            }
                elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
        gameObject.SetActive(false);
    }
}
