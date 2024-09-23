using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Test : MonoBehaviour
{
    private ParticleSystem particleSystem2;
    public float testRange;
    public TestActor testActor;

    void Awake()
    {
        particleSystem2= GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        CheckSize();
    }
    private void Update()
    {
        CheckActor();
    }
    void CheckSize()
    {
        var shape = particleSystem2.shape;
        Debug.Log(shape);
    }
    void CheckActor()
    {
        if (testActor != null)
        {
            Vector3 dir = testActor.transform.position - transform.position;
            float dis = dir.magnitude;
            if (dis < testRange)
            {
                Debug.Log("범위 내로 들어옴");
            }
        }
    }
}