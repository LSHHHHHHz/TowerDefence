using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PopupManager : MonoBehaviour
{
    public ShopPopup shopPopup;
    public AudioSettingUI audioSettingUI;
    public void OpenShopPopup()
    {
        shopPopup.gameObject.SetActive(true);
    }
    public void OpenAudioSettingPopup()
    {
        audioSettingUI.gameObject.SetActive(true);
    }
}