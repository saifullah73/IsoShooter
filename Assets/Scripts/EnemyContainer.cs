using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyContainer
{
    public GameObject character;
    [Range(0,1)]
    public float spawnChance;
    public EnemyType type;
}
