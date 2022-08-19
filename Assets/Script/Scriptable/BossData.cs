using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss_", menuName = "Scriptable/BossData", order = 2)]
public class BossData : ScriptableObject
{
    [Header("Attack")]
    [SerializeField] public bool randomAttack = false;
    [SerializeField] public int movingSkill = -1;
    [SerializeField] public int attackCount = 0;
    [SerializeField] public int currentProcess = 0;
    [SerializeField] public int maxProcess = 100;
    [SerializeField] public List<int> attackProcess;

    [Header("Moving")]
    [SerializeField] public bool bossCanMoving = true;
    [SerializeField] public bool randomMoving = true;
    [SerializeField] public float delayAfterMoving = 0.5f;
    [SerializeField] public string movingSkillName = "MovingSkill";
    [SerializeField] public int minAttackBeforeMoing = 1;
    [SerializeField] public int maxAttackBeforeMoing = 3;
    [SerializeField] public int maxMovingTime = 50;
    [SerializeField] public List<int> movingProcess;
}