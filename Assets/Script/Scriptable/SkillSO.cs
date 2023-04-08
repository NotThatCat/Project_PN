using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "Scriptable/SkillSO", order = 2)]
public class SkillSO : ScriptableObject
{
    [SerializeField] public Sprite image;
    [SerializeField] public SKILL_TYPE skillType = SKILL_TYPE.DEFAULT;
    [Header("Attack Delay")]
    [SerializeField] public bool useDelay = false;
    [SerializeField] public bool delayScaleWithLevel = true;
    [SerializeField] public float baseDelay = 0f;
    [SerializeField] public float minDelay = 0f;
    [SerializeField] public float maxDelay = 10f;

    [Header("Cool Down")]
    [SerializeField] public bool useCoolDown = true;
    [SerializeField] public bool coolDownScaleWithLevel = true;
    [SerializeField] public float baseCoolDown = 0.2f;
    [SerializeField] public float minCoolDown = 0.05f;
    [SerializeField] public float maxCoolDown = 10f;

    [Header("Effect")]
    [SerializeField] public SoundID soundID;
    [SerializeField] public string soundName = "DefaultSound";
    [SerializeField] public string effectName = "DefaulEffect";

    [Header("Attack Info")]
    [SerializeField] public float damage = 1;
    [SerializeField] public bool loopAttack = false;
    [SerializeField] public string bulletName = "DefaultBullet";

    [Header("Spear Control")]
    [SerializeField] public bool isSpearAttack = false;
    [SerializeField] public float spearNumbers = 2;
    [SerializeField] public float bulletRotaFixed = 0.05f;

    [Header("Burst Control")]
    [SerializeField] public bool isBurstAttack = false;
    [SerializeField] public bool useBaseDelay = false;
    [SerializeField] public bool burstDelayScaleWithLevel = false;
    [SerializeField] public int burstNumber = 2;
    [SerializeField] public float burstDelay = 0.1f;


    [Header("Random coolDown")]
    [SerializeField] public bool isRandomCoolDown = false;
    [SerializeField] public float minRanCoolDown = 2f;
    [SerializeField] public float maxRanCoolDown = 5f;

}