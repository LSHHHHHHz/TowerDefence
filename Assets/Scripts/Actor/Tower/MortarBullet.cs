using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBullet : MonoBehaviour , IBullet
{
    public GameObject[] effects;
    Vector3 dir;
    Vector3 targetPos;

    //���� ��ġ�� ��ǥ�� ��ġ�� �߰����ʱ��� �̵� �� �߰���ġ�� �����ϸ� ��ǥ ��ġ�� �̵�
    //�߻������� �Ѱܿ� �����͸����� �Ѿ��� ������, �߻��Ŀ��� ����� ���ӿ�����Ʈ�� ��ġ�� �����̻���ó�� ���� ��� ��
    //Detect�� �����̰� �ִ� ������ ó�� ����� ������ ��� ����
    public void InitializedBullet(Vector3 dir, Vector3 targetPos)
    {
        this.dir = dir;
        this.targetPos = targetPos;
    }
    public void FireBullet()
    {
        Debug.Log("�ڰ��� ������ �����ϳ�?");
    }
    public void ActivateEffect()
    {
        for (int i = 0; i < effects.Length;  i++)
        {
            effects[i].SetActive(true);
        }
    }
    public void DisabledEffect()
    {
        for (int i = 0; i < effects.Length; i++)
        {
            effects[i].SetActive(false);
        }
    }
}
