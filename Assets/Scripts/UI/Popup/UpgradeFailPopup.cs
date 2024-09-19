using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeFailPopup : MonoBehaviour
{
    Image image;
    Text text;
    float elapsedTime = 0;
    float intervalTime = 1;

    private void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
    }
    private void OnEnable()
    {
        elapsedTime = 0;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        StopAllCoroutines();
        StartCoroutine(ClosePopup());
    }
    IEnumerator ClosePopup()
    {
        while (elapsedTime < intervalTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / intervalTime);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
