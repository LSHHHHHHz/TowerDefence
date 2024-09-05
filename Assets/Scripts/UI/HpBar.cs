using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image fillAmountImage;
    public float testMaxHP;
    public float testCurrentHP;
    public float test;
    public void UpdateFillAmount(float maxHp, float currentHp)
    {
        fillAmountImage.fillAmount = currentHp / maxHp;
        testCurrentHP = currentHp;
        testMaxHP = maxHp;
        test = currentHp / maxHp;
        Debug.LogError("들어오나");
    }
    private void LateUpdate()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraUp = Camera.main.transform.up;

        transform.rotation = Quaternion.LookRotation(cameraForward, cameraUp);
    }
}