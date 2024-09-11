using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PopupManager : MonoBehaviour
{
    public ShopPopup shopPopup;

    public void OpenShopPopup()
    {
        shopPopup.gameObject.SetActive(true);
    }
}