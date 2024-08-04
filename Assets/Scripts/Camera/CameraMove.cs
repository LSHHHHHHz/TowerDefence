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
    [SerializeField] float rotationSpeed = 5;
    public float pitch = 0;
    public float yaw = 0;

    Vector3 originPos;
    Quaternion originRot;
    Vector3 lastPos;
    Quaternion lastRot;

    bool isCameraRot = false;
    bool isContactBoundary = false;
    bool isOriginPos = false;
    private void Awake()
    {
        originPos = transform.position;
        originRot = transform.rotation;
    }
    private void Update()
    {
        if(isOriginPos)
        {
            lastPos = transform.position;
            lastRot = transform.rotation;
            transform.position = Vector3.Lerp(transform.position, originPos, 0.3f * Time.deltaTime * 60);
            transform.rotation = Quaternion.Lerp(transform.rotation, originRot, 0.3f * Time.deltaTime * 60);
        }
        else
        {
            MoveCamera();
            RotationCamera();
        }
    }
    void MoveCamera()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        Vector3 move = transform.right * moveX + transform.forward * moveY;
        if (isContactBoundary)
        {
            move = Vector3.zero;
        }
        transform.position += move;

    }
    void RotationCamera()
    {
        float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        yaw += rotX;
        pitch -= rotY;

        pitch = Mathf.Clamp(pitch, -60f, 90f);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }
    public void Test1()
    {
        isOriginPos = false;
    }
    public void Test2()
    {
        isOriginPos = true;
    }
}