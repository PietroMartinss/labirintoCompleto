using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsSceneController : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";
    public float delayBeforeMenu = 8f;
    public bool allowSkip = true;

    private float timer;

    void Start()
    {
        timer = delayBeforeMenu;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f || (allowSkip && Input.anyKeyDown))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}
