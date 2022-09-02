using System;
using UnityEngine;

[Serializable]
public class StateWave
{
    [SerializeField] public WAVE_ID waveId;
    [SerializeField] public float spawnAt = 0;

    public int CompareTo(StateWave item)
    {        // A null value means that this object is greater.
        if (item == null)
        {
            return 1;
        }
        else
        {
            return this.spawnAt.CompareTo(item.spawnAt);
        }
    }
}