using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public EnemyContainer[] enemies;
    public GameObject[] spawnPoints;
    public float timeBetweenSpawns;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, timeBetweenSpawns);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        float totalWeight = 0;
        List<float> weights = new List<float>();
        for (int i = 0; i < enemies.Length; i++)
        {
            totalWeight += enemies[i].spawnChance;
            weights.Add(totalWeight);
        }
        float roll = Random.Range(0f, 1f) * totalWeight;
        for (int i = 0; i < weights.Count; i++)
        {
            if (roll < weights[i])
            {
                Instantiate(enemies[i].character, getSpawnPosition(), Quaternion.identity);
                break;
            }
        }
    }

    Vector3 getSpawnPosition()
    {
        int idx = Random.Range(0,spawnPoints.Length);
        GameObject spawnPoint = spawnPoints[idx];
        //Bounds bounds = spawnPoint.GetComponent<MeshFilter>().mesh.bounds;
        float xMin = spawnPoint.transform.position.x - spawnPoint.transform.localScale.x/2;
        float xMax = spawnPoint.transform.position.x + spawnPoint.transform.localScale.x/2;
        float zMin = spawnPoint.transform.position.z - spawnPoint.transform.localScale.z/2;
        float zMax = spawnPoint.transform.position.z + spawnPoint.transform.localScale.z/2;
        Vector3 newVec = new Vector3(Random.Range(xMin, xMax),spawnPoint.transform.position.y,Random.Range(zMin,zMax));
        return newVec;
    }

    public void changeTimeBetweenSpawns(int seconds)
    {
        timeBetweenSpawns = seconds;
        CancelInvoke("SpawnEnemy");
        InvokeRepeating("SpawnEnemy", 0, timeBetweenSpawns);
    }
    public void raiseSpawnChance(float fodderpercent,float toughPercent)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            float spawnRate = enemies[i].spawnChance;
            if (enemies[i].type == EnemyType.Fodder)
            {
                enemies[i].spawnChance = Mathf.Clamp(spawnRate + spawnRate * fodderpercent, 0, 1);
            }
            else if(enemies[i].type == EnemyType.Tough)
            {
                enemies[i].spawnChance = Mathf.Clamp(spawnRate + spawnRate * toughPercent, 0, 1);
            }
            
        }
    }
}
