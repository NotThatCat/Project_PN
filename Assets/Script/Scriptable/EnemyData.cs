using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    [Header("Drop")]
    [SerializeField] public bool canDrop;
    [SerializeField] public List<DropRate> dropList;

    [Header("HP")]
    [SerializeField] public float hp;

    [Header("Moving")]
    [SerializeField] public float speed;
    [SerializeField] public float baseMovingSpeed;
    [SerializeField] public List<AccelerationData> acceleration;
}