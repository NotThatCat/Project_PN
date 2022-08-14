using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCtrl : PMonoBehaviour
{

    [Header("Model")]
    [SerializeField] public PlaneAnimator planeAnimator;

    //Controller
    [Header("Attack")]
    [SerializeField] public AttackCtrl attackCtrl;
    [SerializeField] public List<Transform> skills;
    [SerializeField] public List<SkillCtrl> skillCtrls;

    [Header("Level")]
    [SerializeField] public Level level;
    //Level display is not sync yet
    [SerializeField] public int currentLevel;
    [SerializeField] public int maxLevel;

    //protected override void Awake()
    //{
    //    base.Awake();
    //    this.LoadComponents();
    //}

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
        this.LoadAnimator();
    }

    protected virtual void LoadCtrl()
    {
        this.LoadAttackCtrl();
        this.LoadLevel();
    }
    protected virtual void LoadAttackCtrl()
    {
        this.attackCtrl = transform.GetComponentInChildren<AttackCtrl>();
        this.skills = this.attackCtrl.skills;
        this.skillCtrls = this.attackCtrl.skillCtrls;
    }

    protected virtual void LoadLevel()
    {
        this.level = transform.GetComponentInChildren<Level>();
        this.currentLevel = level.level;
        this.maxLevel = level.maxLevel;
    }

    protected virtual void LoadAnimator()
    {
        this.planeAnimator = transform.GetComponentInChildren<PlaneAnimator>();
    }

}
