using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerUpContainer
{
    public GameObject instance;
    [Range(0, 1)]
    public float spawnChance;
}
