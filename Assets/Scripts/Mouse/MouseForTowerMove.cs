using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MouseForTowerMove : MonoBehaviour
{
    private Vector3 m_Offset;
    private float m_ZCoord;
    public GameObject test;

    void OnMouseDown()
    {
        Debug.Log("되나");
        m_ZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        m_Offset = gameObject.transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        Debug.Log("되나");
        transform.position = GetMouseWorldPosition() + m_Offset;
        test.transform.position = transform.position;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = m_ZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}