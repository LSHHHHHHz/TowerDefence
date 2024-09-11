using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class CountDownPopup : MonoBehaviour
{
    [SerializeField] Text countDownText;
    int countdownDuration = 3;
    public event Action onPossibleNextStage;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StartCoroutine(StartCountDown(countdownDuration));
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator StartCountDown(int count)
    {
        float elapsedTime = count;
        while (elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
            countDownText.text = Mathf.Ceil(elapsedTime).ToString();
            yield return null;
        }
        countDownText.text = "0";
        yield return new WaitForSeconds(1);
        countDownText.text = "Start!";
        onPossibleNextStage?.Invoke();
        gameObject.SetActive(false);
    }
}