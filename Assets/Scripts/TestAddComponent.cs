using System;
using UnityEngine;

public class TestAddComponent : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddComponent<Monster>();
    }
}
