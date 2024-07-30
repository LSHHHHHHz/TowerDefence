using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    Tower tower;
    bool findMonster;
    Coroutine attackCoroutine;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;


        Gizmos.DrawWireSphere(transform.position, 30); //타워 데이터 만들고 다시작업
    }
    private void Awake()
    {
        tower = GetComponent<Tower>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (findMonster)
            {
                findMonster = false;
                StopAttack();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (!findMonster)
            {
                findMonster = true;
                StartAttack(2);
            }
        }
    }
    private void StartAttack(float attackSpeed)
    {
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(StartBaseAttackCoroutine(2f));
        }
    }

    private void StopAttack()
    {
        if (attackCoroutine != null) 
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }
    IEnumerator StartBaseAttackCoroutine(float attackSpeed)
    {
        while (true)
        {
            StartAttackAction();
            yield return new WaitForSeconds(attackSpeed);
        }
    }
    void DetectActor(Vector3 center, float radius, string tag)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            hitColliders[i].SendMessage("AddDamage");
            i++;
        }
    }
    void StartAttackAction()
    {
        Debug.Log("공격!!!");
    }
}
