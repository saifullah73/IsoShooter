using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;
    public static bool isGamePaused = false;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
    }

    public void RestartGame()
    {
        isGamePaused = false;
        SceneManager.LoadScene(1);
        UIManager.instance.InitializeGame();
    }

    public void UpdateDeath()
    {
        UIManager.instance.UpdateDeathUI();
        PauseGame();
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
        //StartCoroutine(Pause(true));
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        //StartCoroutine(Pause(false));
    }


    private IEnumerator Pause(bool flag)
    {
        yield return new WaitForSeconds(1f);
        if (flag)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void QuitGame()
    {
# if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        
    }
}
