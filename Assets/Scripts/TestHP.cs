using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHP : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}