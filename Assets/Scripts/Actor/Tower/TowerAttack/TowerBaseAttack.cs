using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class TowerBaseAttack : MonoBehaviour
{
    Tower tower;
    Coroutine attackCoroutine;
    float initializedAttackDelay;
    int attackAmount;
    public string projectilePath;
    [SerializeField] float resetTime = 0.4f;
    Transform firePos;
    private bool isReadyToAttack = false;
    private Vector3 targetPos;
    public BaseProjectile projectile;
    public event Action isAttackActionFalse;
    public event Action isAttackActionTrue;
    private void Awake()
    {
        tower = GetComponent<Tower>();
    }
    private void Start()
    {
        projectilePath = gameObject.GetComponent<Tower>().profileDB.projectilePath;
    }
    public void Initialize(Transform firePos, int attackSpeed, int amount)
    {
        this.firePos = firePos;
        initializedAttackDelay = attackSpeed;
        attackAmount = amount;
    }
    public void StartAttack(IActor targetActor)
    {
        if (isReadyToAttack && attackCoroutine == null) 
        {
            attackCoroutine = StartCoroutine(AttackCoroutine(targetActor));
            isAttackActionTrue?.Invoke();
            Debug.Log("아아");
        }
    }
    public void StopAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
        isAttackActionFalse?.Invoke();
        Debug.Log("StopAttack");
    }
    public void SetReadyToAttack(bool ready, Vector3 targetPos)
    {
        isReadyToAttack = ready;
        if (ready)
        {
            this.targetPos = targetPos;
        }
    }
    IEnumerator AttackCoroutine(IActor targetActor)
    {
        isAttackActionTrue?.Invoke();
        StartAttackAction(targetActor);
        //while (targetActor != null)
        //{
        //    attackDelay -= Time.deltaTime;
        //    if (attackDelay <= 0)
        //    {
        //        yield return new WaitForSeconds(resetTime);
        //        attackDelay = initializedAttackDelay;
        //    }
        //    yield return null;
        //}
        yield return new WaitForSeconds(0.2f);
        tower.fsmController.ChangeState(new AttackState());
        yield return new WaitForSeconds(resetTime);
        StopAttack();
    }
    void StartAttackAction(IActor target)
    {
        FireProjectile(firePos.position, targetPos, target);
    }
    public void FireProjectile(Vector3 firePos, Vector3 targetPos, IActor target)
    {
        Debug.Log("몇번");
        BaseProjectile projectile = null;
        if (this.projectile == null)
        {
            projectile = PoolManager.instance.GetObjectFromPool(projectilePath).GetComponent<BaseProjectile>();
        }
        else
        {
            projectile = this.projectile;
        }
        if (projectile != null)
        {
            projectile.InitializedProjectile(firePos, attackAmount, target);
            projectile.MoveTarget(targetPos, target);
        }
    }
}