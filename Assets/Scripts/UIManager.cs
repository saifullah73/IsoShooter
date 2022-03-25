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
    public Button PauseHomeButton;


    public Button SettingsBack;
    public Slider SettingsSlider;

    public AudioListener Audiolistener;

    public Animator CameraAnimator;




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
        InitializeGame();
    }


    void GoToSettings()
    {
        AudioManager.instance.PlayUISoundEffect();
        Debug.Log("Going to setting");
        UIAnimator.SetBool("Setting",true);
        CameraAnimator.SetBool("Setting", true);
        SetSettingGroupActive(true);
        SetExitGroupActive(false);
        SetHomeGroupActive(false);
        SetHudGroupActive(false);
        SetPauseGroupActive(false);
    }

    void SettingsToHome()
    {
        AudioManager.instance.PlayUISoundEffect();
        Debug.Log("Going to home");
        UIAnimator.SetBool("Setting", false);
        CameraAnimator.SetBool("Setting", false);
        SetSettingGroupActive(false);
        SetExitGroupActive(false);
        SetHomeGroupActive(true);
        SetHudGroupActive(false);
        SetPauseGroupActive(false);
    }

    void VolumeChanged(float value)
    {
        Debug.Log("Chaging audio");
        AudioListener.volume = value;
    }


    public void UpdateDeathUI()
    {
        UIAnimator.SetTrigger("HudToDeath");
        SetSettingGroupActive(false);
        SetExitGroupActive(true);
        SetHomeGroupActive(false);
        SetHudGroupActive(false);
        SetPauseGroupActive(false);
        DieTimerText.text = timerText;
    }

    public void PauseUI()
    {
        AudioManager.instance.PlayUISoundEffect();
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
        AudioManager.instance.PlayUISoundEffect();
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
        AudioManager.instance.PlayUISoundEffect();
        Debug.Log("Quitting Game");
        SceneManagement.instance.QuitGame();
    }

    void RestartGame()
    {
        AudioManager.instance.PlayUISoundEffect();
        Debug.Log("Restarting Game");
        SceneManagement.instance.RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InitializeGame()
    {
        SceneManagement.instance.PauseGame();
        HomeStartGameButton.onClick.AddListener(StartGame);
        HomeExitButton.onClick.AddListener(ExitGame);
        DieGoToHomeButton.onClick.AddListener(RestartGame);
        DieExitButton.onClick.AddListener(ExitGame);
        PauseButton.onClick.AddListener(PauseUI);
        ResumeButton.onClick.AddListener(ResumeUI);
        PauseHomeButton.onClick.AddListener(RestartGame);
        HomeSettingButton.onClick.AddListener(GoToSettings);
        SettingsBack.onClick.AddListener(SettingsToHome);
        SettingsSlider.onValueChanged.AddListener(VolumeChanged);
        Debug.Log("Initializing Game");
        SetSettingGroupActive(false);
        SetExitGroupActive(false);
        SetHomeGroupActive(true);
        SetHudGroupActive(false);
        SetPauseGroupActive(false);
    }

    void StartGame()
    {
        AudioManager.instance.PlayGameStartAudio();
        Debug.Log("Starting Game");
        UIAnimator.SetTrigger("HomeToHud");
        SetSettingGroupActive(false);
        SetExitGroupActive(false);
        SetHomeGroupActive(false);
        SetHudGroupActive(true);
        SetPauseGroupActive(false);
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
