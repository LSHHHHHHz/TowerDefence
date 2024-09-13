using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoad : MonoBehaviour
{
    public void TowerDefenceLoad()
    {
        LoadingSceneController.Instance.LoadScene("TowerDefence");
    }
}
