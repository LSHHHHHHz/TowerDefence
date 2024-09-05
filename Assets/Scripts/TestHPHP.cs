using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHPHP : MonoBehaviour
{
    //�ƽ� HP�� �ʿ�
    //HP�� �����Ǿ� ���� �������� �Ǿ����� ��
    //�����Ͱ� ������ HP�ٸ� ����
    [SerializeField] Monster monster;
    [SerializeField] Image hpBarImage;
    Camera mainCamera = null;
    int maxHP = 0;
    int currentHp = 0;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    public void OnDamaged(int hp)
    {
        if(maxHP ==0)
        {
            maxHP = hp;
            currentHp = hp;
        }
    }
    public void StatusHpBar(int amount)
    {
        hpBarImage.fillAmount = (currentHp - amount) / maxHP;
    }
    private void Update()
    {
        transform.position = monster.gameObject.transform.position + new Vector3(0,10,0);
    }

}
