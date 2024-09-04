using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    int currentStage = 1;
    ActorType currenntActorType;
    int maxMonsterCount;
    int currentMonsterCount = 10;
    int clearMonsterCount = 0;
    float stageClearAfterElapsedTime = 5f;
    bool isClearStage = false;
    private void Update()
    {
        CheckStageClear();
    }
    void CheckStageClear()
    {
        if (currentMonsterCount <= clearMonsterCount && !isClearStage)
        {
            isClearStage = true;
            StartCoroutine(HandleStageClear());
        }
    }
    IEnumerator HandleStageClear()
    {
        Debug.LogError("�������� Ŭ����!");

        yield return new WaitForSeconds(stageClearAfterElapsedTime);

        StartNextStage();
    }
    void StartNextStage()
    {
        currentStage++;
        isClearStage = false;
        Debug.LogError("���� �������� ����!");
    }
}