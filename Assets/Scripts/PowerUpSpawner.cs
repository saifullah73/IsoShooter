using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public PowerUpContainer[] powerUps;
    public GameObject[] spawnPoints;
    public float timeBetweenSpawns;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPowerUp", 0, timeBetweenSpawns);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnPowerUp()
    {
        float totalWeight = 0;
        List<float> weights = new List<float>();
        for (int i = 0; i < powerUps.Length; i++)
        {
            totalWeight += powerUps[i].spawnChance;
            weights.Add(totalWeight);
        }
        float roll = Random.Range(0f, 1f) * totalWeight;
        for (int i = 0; i < weights.Count; i++)
        {
            if (roll < weights[i])
            {
                Instantiate(powerUps[i].instance, getSpawnPosition(), powerUps[i].instance.transform.rotation);
                break;
            }
        }
    }

    Vector3 getSpawnPosition()
    {
        int idx = Random.Range(0, spawnPoints.Length);
        GameObject spawnPoint = spawnPoints[idx];
        float xMin = spawnPoint.transform.position.x - spawnPoint.transform.localScale.x / 2;
        float xMax = spawnPoint.transform.position.x + spawnPoint.transform.localScale.x / 2;
        float zMin = spawnPoint.transform.position.z - spawnPoint.transform.localScale.z / 2;
        float zMax = spawnPoint.transform.position.z + spawnPoint.transform.localScale.z / 2;
        Vector3 newVec = new Vector3(Random.Range(xMin, xMax),1, Random.Range(zMin, zMax));
        return newVec;
    }

}
