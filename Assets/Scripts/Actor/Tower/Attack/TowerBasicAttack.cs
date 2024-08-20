using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBasicAttack : MonoBehaviour
{
    public GameObject[] effects;
    Vector3 dir;
    Vector3 targetPos;

    //���� ��ġ�� ��ǥ�� ��ġ�� �߰����ʱ��� �̵� �� �߰���ġ�� �����ϸ� ��ǥ ��ġ�� �̵�
    //�߻������� �Ѱܿ� �����͸����� �Ѿ��� ������, �߻��Ŀ��� ����� ���ӿ�����Ʈ�� ��ġ�� �����̻���ó�� ���� ��� ��
    //Detect�� �����̰� �ִ� ������ ó�� ����� ������ ��� ����
    public void FindTargetPos(Vector3 dir, Vector3 targetPos)
    {
        this.dir = dir;
        this.targetPos = targetPos;
    }
    public abstract void FireBullet();
}
