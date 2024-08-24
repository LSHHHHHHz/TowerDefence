using System;
using UnityEngine;

public class TestTower : MonoBehaviour
{
    public event Action OnLevelUp;

    private void LevelUp()
    {
        OnLevelUp?.Invoke();
    }
}
public class TestLevelUpManager
{
    private TestPopup popup;

    public TestLevelUpManager()
    {

    }

    private void NotifyLevelUp()
    {
        popup.LevelUP();
    }
}
public class TestPopup : MonoBehaviour
{
    public void LevelUP()
    {
        Debug.Log("·¹º§ ¾÷ ÆË¾÷");
    }
}
