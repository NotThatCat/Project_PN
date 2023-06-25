using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : PMonoBehaviour
{

    private static WaveSpawner instance;
    public static WaveSpawner Instance => instance;


    protected override void Awake()
    {
        base.Awake();
        if (WaveSpawner.instance != null) Debug.LogError("Only 1 EnemySpawner allow to exist");
        WaveSpawner.instance = this;
    }

}
