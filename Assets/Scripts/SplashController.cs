using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    public GameObject loadingBar;
    public Image fillImage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());   
    }


    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3);
        loadingBar.SetActive(true);
        yield return new WaitForSeconds(1);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        while (!asyncLoad.isDone)
        {
            fillImage.fillAmount = asyncLoad.progress;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
