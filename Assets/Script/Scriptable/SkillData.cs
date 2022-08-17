using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable/Skill", order = 2)]
public class SkillData : ScriptableObject
{
    [Header("Attack Delay")]
    [SerializeField] public float baseDelay = 0f;
    [SerializeField] public float finalDelay = 0f;
    [SerializeField] public float minDelay = 0f;
    [SerializeField] public float maxDelay = 2f;

    [Header("Cool Down")]
    [SerializeField] public float baseCoolDown = 0.2f;
    [SerializeField] public float finalCoolDown = 0.2f;
    [SerializeField] public float minCoolDown = 0.05f;
    [SerializeField] public float maxCoolDown = 0.2f;

    [Header("Attack Info")]
    [SerializeField] public float damage = 1;
    [SerializeField] public string bulletName = "DefaultBullet";
    [SerializeField] public SKILL_TYPE type = SKILL_TYPE.DEFAULT;

    [Header("Spear Control")]
    [SerializeField] public bool useSpearNumbers = true;
    [SerializeField] public float spearNumbers = 2;
    [SerializeField] public float bulletRotaFixed = 0.05f;

    [Header("Wave Control")]
    [SerializeField] public int number = 3;
}