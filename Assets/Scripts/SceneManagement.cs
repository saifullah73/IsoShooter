using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
