using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CameraMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    int minX = -2;
    int maxX = 2;
    int minZ = 0;
    int maxZ = 5;
    private void Update()
    {
        MoveCamera();
    }
    void MoveCamera()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        Vector3 move = transform.right * -moveX + transform.forward * -moveZ;
        transform.position += move;

        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(clampedX, 0, clampedZ);
    }
}