using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private Image coinImage;
    Color originColor;
    Vector3 originLocalPos; 
    private void Awake()
    {
        coinImage = GetComponent<Image>();
        originColor = coinImage.color;
        originLocalPos = transform.localPosition;
    }
    private void OnEnable()
    {
        coinImage.color = originColor;
        transform.localPosition = originLocalPos;
        StartCoroutine(HandleImage());
    }
    IEnumerator HandleImage()
    {
        Sequence imageSequence = DOTween.Sequence();
        imageSequence.Append(transform.DOLocalMoveY(3.5f, 1f)); 
        imageSequence.Join(coinImage.DOFade(0f, 1f)); 
        yield return imageSequence.WaitForCompletion();

        gameObject.SetActive(false);
    }
    private void LateUpdate()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraUp = Camera.main.transform.up;

        transform.rotation = Quaternion.LookRotation(cameraForward, cameraUp);
    }
}