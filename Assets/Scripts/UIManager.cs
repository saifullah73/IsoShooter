using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private string timerText;
    public static UIManager instance;
    public Animator UIAnimator;

    public CanvasGroup HomeScreenGroup;
    public CanvasGroup ExitGroup;
    public CanvasGroup SettingGroup;
    public CanvasGroup HudGroup;
    public CanvasGroup PauseGroup;



    public Image healthBar;
    public TMPro.TMP_Text timer;
    public Image TimerImage;


    public Button HomeStartGameButton;
    public Button HomeSettingButton;
    public Button HomeExitButton;


    public Button DieGoToHomeButton;
    public Button DieExitButton;
    public TMP_Text DieTimerText;

    public Button PauseButton;
    public Button ResumeButton;




    public void Awake()
    {
        instance = this;
    }
    public static UIManager getInstance()
    {
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManagement.instance.PauseGame();
        HomeStartGameButton.onClick.AddListener(StartGame);
        HomeExitButton.onClick.AddListener(ExitGame);
        DieGoToHomeButton.onClick.AddListener(RestartGame);
        DieExitButton.onClick.AddListener(ExitGame);
        PauseButton.onClick.AddListener(PauseUI);
        ResumeButton.onClick.AddListener(ResumeUI);
        InitializeGame();
    }


    public void UpdateDeathUI()
    {
        UIAnimator.SetTrigger("HudToDeath");
        SetSettingGroupActive(false);
        SetExitGroupActive(true);
        SetHomeGroupActive(false);
        SetHudGroupActive(false);
        DieTimerText.text = timerText;
    }

    public void PauseUI()
    {
        Debug.Log("Pausing Game");
        UIAnimator.SetTrigger("Pause");
        SetSettingGroupActive(false);
        SetExitGroupActive(false);
        SetHomeGroupActive(false);
        SetHudGroupActive(false);
        SetPauseGroupActive(true);
        SceneManagement.instance.PauseGame();
    }

    public void ResumeUI()
    {
        Debug.Log("Resuming Game");
        UIAnimator.SetTrigger("Resume");
        SetSettingGroupActive(false);
        SetExitGroupActive(false);
        SetHomeGroupActive(false);
        SetHudGroupActive(true);
        SetPauseGroupActive(false);
        SceneManagement.instance.ResumeGame();
    }

    void ExitGame()
    {
        Debug.Log("Quitting Game");
        //SceneManagement.instance.QuitGame();
    }

    void RestartGame()
    {
        Debug.Log("Restarting Game");
        SceneManagement.instance.RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManagement.instance.PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManagement.instance.ResumeGame();
        }
    }

    void InitializeGame()
    {
        Debug.Log("Initializing Game");
        SetSettingGroupActive(false);
        SetExitGroupActive(false);
        SetHomeGroupActive(true);
        SetHudGroupActive(false);
    }

    void StartGame()
    {
        Debug.Log("Starting Game");
        UIAnimator.SetTrigger("HomeToHud");
        SetSettingGroupActive(false);
        SetExitGroupActive(false);
        SetHomeGroupActive(false);
        SetHudGroupActive(true);
        SceneManagement.instance.ResumeGame();
    }

    public void SetPowerUpTimer(float timeSinceLastSpawn, float timeBetweenSpawns)
    {
        float percentageTime = (timeBetweenSpawns-timeSinceLastSpawn)/ timeBetweenSpawns;
        TimerImage.fillAmount = percentageTime;
    }

    public void UpdateTimer(int minutes, int seconds)
    {
        string min = minutes.ToString();
        string sec = seconds.ToString();
        if (minutes < 10)
        {
            min = "0" + min;
        }
        if (seconds < 10)
        {
            sec = "0" + sec;
        }
        timerText = min + ":" + sec;
        timer.text = timerText;
    }

    public void updateHealth(float currentHealth, float totalHealth)
    {
        float percentageHealthLeft = currentHealth / totalHealth;
        healthBar.fillAmount = percentageHealthLeft;
    }


    void SetHomeGroupActive(bool flag)
    {
        if (!flag)
        {
            HomeScreenGroup.interactable = false;
            HomeScreenGroup.blocksRaycasts = false;
        }
        else
        {
            HomeScreenGroup.interactable = true;
            HomeScreenGroup.blocksRaycasts = true;
        }
    }

    void SetExitGroupActive(bool flag)
    {
        if (!flag)
        {
            ExitGroup.interactable = false;
            ExitGroup.blocksRaycasts = false;
        }
        else
        {
            ExitGroup.interactable = true;
            ExitGroup.blocksRaycasts = true;
        }
    }

    void SetHudGroupActive(bool flag)
    {
        if (!flag)
        {
            HudGroup.interactable = false;
            HudGroup.blocksRaycasts = false;
        }
        else
        {
            HudGroup.interactable = true;
            HudGroup.blocksRaycasts = true;
        }
    }

    void SetSettingGroupActive(bool flag)
    {
        if (!flag)
        {
            SettingGroup.interactable = false;
            SettingGroup.blocksRaycasts = false;
        }
        else
        {
            SettingGroup.interactable = true;
            SettingGroup.blocksRaycasts = true;
        }
    }

    void SetPauseGroupActive(bool flag)
    {
        if (!flag)
        {
            PauseGroup.interactable = false;
            PauseGroup.blocksRaycasts = false;
        }
        else
        {
            PauseGroup.interactable = true;
            PauseGroup.blocksRaycasts = true;
        }
    }

}
