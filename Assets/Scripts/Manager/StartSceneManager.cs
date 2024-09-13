using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartSceneManager : MonoBehaviour
{
    [SerializeField] RectTransform startButtonImage;
    [SerializeField] RectTransform exitButtonImage;

    private Sequence startButtonSequence;
    private Sequence exitButtonSequence;

    private void Awake()
    {
        startButtonSequence = AnimationImage(startButtonImage, 1.05f);
        exitButtonSequence = AnimationImage(exitButtonImage, 1.05f);
    }
    public void StartTowerDefence()
    {
        startButtonSequence.Kill();
        exitButtonSequence.Kill();
        LoadingSceneController.Instance.LoadScene("TowerDefence");
    }
    Sequence AnimationImage(RectTransform image, float scale)
    {
        Vector3 originalScale = image.localScale;
        Sequence imageSequence = DOTween.Sequence();
        imageSequence.Append(image.DOScale(scale, 0.3f))
                     .Append(image.DOScale(originalScale, 0.5f))
                     .SetLoops(-1, LoopType.Yoyo);
        return imageSequence;
    }
}
