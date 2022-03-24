using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public GameObject missile;
    public float timeBetweenSpawns;
    private GameObject player;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        timer = timeBetweenSpawns;
        //InvokeRepeating("SpawnMissile",timeBetweenSpawns,timeBetweenSpawns);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            SpawnMissile();
            timer = timeBetweenSpawns;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        UIManager.instance.SetPowerUpTimer(timer, timeBetweenSpawns);
    }


    void SpawnMissile()
    {
        Vector3 pos = player.transform.position;
        float x = pos.x + Random.Range(-5f, 5f);
        float z = pos.z + Random.Range(-5f, 5f);
        pos.x = x;
        pos.z = z;
        pos.y = 10f;
        Instantiate(missile, pos, Quaternion.identity);

    }

    public void changeSpawnRate(float time)
    {
        timeBetweenSpawns = time;
        CancelInvoke("SpawnMissile");
        InvokeRepeating("SpawnMissile", timeBetweenSpawns,timeBetweenSpawns);
    }
}
