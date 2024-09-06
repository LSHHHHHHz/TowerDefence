using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoinAction : MonoBehaviour
{
    float posOffset = 1;
    float rotOffset = 1080;
    float duration = 1f;
    Vector3 originPos;
    Quaternion originRot;
    private void Awake()
    {
        originPos = transform.position;
        originRot = transform.rotation;
        //transform.rotation = Quaternion.Euler(0, 1010, 0);
        Quaternion targetRotation = Quaternion.Euler(0, 1010, 0);
        transform.rotation = targetRotation;
        Debug.LogError(" Quaternion.Euler(0, 1080, 0); : " +transform.rotation.eulerAngles);
    }
    private void OnEnable()
    {
        transform.position = originPos;
        transform.rotation = originRot;
        //StartCoroutine(HandleCoinActive());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator HandleCoinActive()
    {
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        Vector3 targetPos = startPos + new Vector3(0, posOffset, 0);
        Quaternion targetRot = Quaternion.Euler(startRot.eulerAngles + new Vector3(0, rotOffset, 0));

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            Debug.LogError("transform.position : "+ transform.position);
            Debug.LogError("transform.rotation : " + transform.rotation);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        transform.rotation = targetRot;
    }
}
