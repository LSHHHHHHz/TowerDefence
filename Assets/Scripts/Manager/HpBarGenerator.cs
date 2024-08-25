using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HpBarGenerator : MonoBehaviour
{
    //타입에 따라 크기 변경
    [SerializeField] HpBar HpBarPrefab = null;
    Monster monster;
    Canvas mainCanvas = null;
    Camera mainCamera = null;
    Transform HpBarTransform;
    HpBar hpbar;
    void Start()
    {
        monster = GetComponent<Monster>();
        mainCamera = Camera.main;
        mainCanvas = FindObjectOfType<Canvas>();
        HpBarTransform = GameObject.Find("HpBarTransform").transform;
        if (hpbar == null)
        {
            hpbar = Instantiate(HpBarPrefab, HpBarTransform).GetComponent<HpBar>();
        }
        monster.onDamagedAction += OnDamaged;
    }
    void LateUpdate()
    {
        hpbar.transform.position = mainCamera.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0));
    }
    void OnDamaged(int hp, int amount)
    {
        HpBarPrefab.OnDamaged(hp);
        HpBarPrefab.StatusHpBar(amount);
    }
}