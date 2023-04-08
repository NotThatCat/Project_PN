using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : Spawner
{
    private static EffectSpawner instance;
    public static EffectSpawner Instance => instance;

    protected override void Awake()
    {
        if (EffectSpawner.instance != null) Debug.Log("Only allow one EffectManager");
        EffectSpawner.instance = this;
    }

}
