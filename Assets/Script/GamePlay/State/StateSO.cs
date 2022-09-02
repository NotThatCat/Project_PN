using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State_", menuName = "Scriptable/StateSO", order = 1)]
public class StateSO : ScriptableObject
{
    [SerializeField] public STATE_ID stateId;
    [SerializeField] public List<StateWave> stateWaves;
}