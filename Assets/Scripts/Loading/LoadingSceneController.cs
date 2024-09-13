using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingSceneController : MonoBehaviour
{
    private static LoadingSceneController instance;
    public static LoadingSceneController Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<LoadingSceneController>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }
            return instance;
        }
    }
    private static LoadingSceneController Create()
    {
        return Instantiate(Resources.Load<LoadingSceneController>("LoadingUI"));
    }
    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image progressBar;
    private string loadSceneName;
    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        loadSceneName = sceneName;
        StartCoroutine(LoadSceneProcess());
    }
    private IEnumerator LoadSceneProcess()
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true));

        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
                Debug.Log(" op.progress < 0.9f : " + op.progress);
            }
            else
            {
                Debug.Log(" else : " + op.progress);
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, op.progress / 10);
                if (progressBar.fillAmount >= 0.9f)
                {
                    Debug.Log(" progressBar.fillAmount >= 1f : " + op.progress / 10);
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
            yield return null;
        }
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;
        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 3f;
            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);
        }

        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }
}
