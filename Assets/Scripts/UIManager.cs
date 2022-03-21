using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Image healthBar;
    public TMPro.TMP_Text timer;
    public static UIManager instance;
    public Button dashButton;

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
        dashButton.onClick.AddListener(() =>
        {
            PlayerController.instance.Dash();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
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
        timer.text = min + ":" + sec;
    }

    public void updateHealth(float currentHealth, float totalHealth)
    {
        float percentageHealthLeft = currentHealth / totalHealth;
        healthBar.fillAmount = percentageHealthLeft;
    }


}
