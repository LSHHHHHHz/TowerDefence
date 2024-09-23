using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDetector : MonoBehaviour
{
    protected float elapsedTime = 0;
    protected float intervalTime = 0.05f;
    private float _detectionRange = 1;

    public float detectionRange
    {
        get { return _detectionRange; }
        set { _detectionRange = value; }
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= intervalTime)
        {
            UpdateDetection();
            elapsedTime = 0;
        }
    }
    protected abstract void UpdateDetection();
}
