using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    private UIManager uiManager;
    private int currentDifficulty;
    private float lastMin;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        uiManager = GetComponent<UIManager>();
        currentDifficulty = 1;
        lastMin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = (int)(Time.time / 60);
        int seconds = (int)Time.time % 60;
        uiManager.UpdateTimer(minutes, seconds);
        if (lastMin != minutes)
        {
            BumpDifficulty(minutes, seconds);
            lastMin = minutes;
        }
            
    }


    void BumpDifficulty(int minutes,int seconds)
    {
        switch (seconds)
        {
            case (1):
                enemySpawner.raiseSpawnChance(0.2f,1f);
                enemySpawner.changeTimeBetweenSpawns(5);
                break;
            case (2):
                enemySpawner.raiseSpawnChance(0.1f,1f);
                enemySpawner.changeTimeBetweenSpawns(4);
                break;
            //case (3):
            //    break;
            //case (4):
            //    break;
            //case (5):
            //    break;
            default:
                break;

        }
    }
}