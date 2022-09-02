using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable/WaveData", order = 1)]
public class WaveData : ScriptableObject
{
    [SerializeField] public WAVE_ID waveId;

    [SerializeField] public float delayBeforeSpawn = 0f;
    [SerializeField] public float spawnBetweenDelay = 0.3f;
    [SerializeField] public float delayAfterSpawn = 0f;
    [SerializeField] public List<string> enemyList;
    [SerializeField] public string pathName;
}