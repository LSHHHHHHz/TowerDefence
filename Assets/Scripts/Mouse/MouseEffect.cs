using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseEffect : MonoBehaviour
{
    RectTransform rectTransform;

    [SerializeField] GameObject[] subEffect;
    List<RectTransform> subRectTransforms;
    List<Image> subImages;

    float mainCircleDuration = 0.3f;
    Vector2 mainCircleOriginSize;

    int subEffectCount;
    float subCircleDuration = 0.1f;
    Vector2 subCircleOriginSize;
    Vector2 subCircleCurrenttSize;
    Color subCircleOriginColor;
    Color subCircleCurrentColor;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        mainCircleOriginSize = rectTransform.sizeDelta;
        subEffectCount = subEffect.Length;
        subRectTransforms = new List<RectTransform>();
        subImages = new List<Image>();
        for (int i = 0; i < subEffect.Length; i++)
        {
            RectTransform subRectTransform = subEffect[i].GetComponent<RectTransform>();
            subRectTransforms.Add(subRectTransform);

            Image subImage = subEffect[i].GetComponent<Image>();
            subImages.Add(subImage);
        }

        subCircleOriginSize = new Vector2(0, 0);
        subCircleOriginColor = new Color(1, 1, 1, 1);
    }
    private void OnEnable()
    {
        subCircleCurrenttSize = subCircleOriginSize;
        subCircleCurrentColor = subCircleOriginColor;
        for(int i =0; i< subEffect.Length; i++)
        {
            subEffect[i].gameObject.SetActive(false);
        }
        StartCoroutine(MainCircleScaleDown());
        StartCoroutine(SubCircleScaleUp());
    }

    private IEnumerator MainCircleScaleDown()
    {
        Vector2 targetSize = new Vector2(0, 0);
        float time = 0;

        while (time < mainCircleDuration)
        {
            rectTransform.sizeDelta = Vector2.Lerp(mainCircleOriginSize, targetSize, time / mainCircleDuration);
            time += Time.deltaTime;
            yield return null;
        }
        rectTransform.sizeDelta = targetSize;
        gameObject.SetActive(false);
    }
    private IEnumerator SubCircleScaleUp()
    {
        List<int> randomNumList = GetRandomNum();
        Vector2 targetSize = new Vector2(50, 50);

        for (int i = 0; i < randomNumList.Count; i++)
        {
            float time = 0;
            while (time < subCircleDuration)
            {
                subEffect[randomNumList[i]].gameObject.SetActive(true);
                subRectTransforms[randomNumList[i]].sizeDelta = Vector2.Lerp(subCircleCurrenttSize, targetSize, time / subCircleDuration);
                subCircleCurrentColor.a = Mathf.Lerp(1, 0.5f, time / subCircleDuration);
                subImages[randomNumList[i]].color = subCircleCurrentColor;
                time += Time.deltaTime;
                yield return null;
            }
            subCircleCurrentColor.a = 0;
            subImages[randomNumList[i]].color = subCircleCurrentColor;
        }
    }
    List<int> GetRandomNum()
    {
        List<int> numbers = new List<int>();
        for (int i = 0; i < subEffectCount; i++)
        {
            numbers.Add(i);
        }
        for (int i = 0; i < numbers.Count; i++)
        {
            int randomNum = UnityEngine.Random.Range(i, numbers.Count);
            int temp = numbers[i];
            numbers[i] = numbers[randomNum];
            numbers[randomNum] = temp;
        }
        return numbers;
    }
}
