using UnityEngine;
using System;

[Serializable]
public class DropRate
{
    [SerializeField] public string itemName;
    [SerializeField] public int dropRate;
    [SerializeField] public int minDrop;
    [SerializeField] public int maxDrop;
}
