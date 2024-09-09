using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEndGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster != null)
            {
                Debug.LogError("MonsterEndGate");
                monster.gameObject.SetActive(false);
            }
        }
    }
}
