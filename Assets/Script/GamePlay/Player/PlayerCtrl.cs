using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : PMonoBehaviour
{
    [SerializeField] public static PlayerCtrl instance;
    [SerializeField] public PlayerMoving playerMovingCtrl;
    [SerializeField] public PlayerAttackCtrl playerAttackCtrl;
    [SerializeField] public PlayerPlaneCtrl playerPlaneCtrl;
    [SerializeField] public Level level;

    protected override void Awake()
    {
        if (PlayerCtrl.instance != null) Debug.LogError("Only 1 PlayerCtrl allow", gameObject);
        PlayerCtrl.instance = this;
    }

    public virtual int GetMaxLevel()
    {
        return this.level.maxLevel;
    }

    public virtual int GetCurrentLevel()
    {
        return this.level.level;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerPlaneCtrl();
        this.LoadPlayerMoving();
        this.LoadPlayerAttack();
        this.LoadLevel();
    }

    protected virtual void LoadLevel()
    {
        this.level = transform.GetComponentInChildren<Level>();
    }

    protected virtual void LoadPlayerPlaneCtrl()
    {
        this.playerPlaneCtrl = transform.GetComponentInChildren<PlayerPlaneCtrl>();
    }

    protected virtual void LoadPlayerMoving()
    {
        this.playerMovingCtrl = transform.GetComponentInChildren<PlayerMoving>();
    }

    protected virtual void LoadPlayerAttack()
    {
        this.playerAttackCtrl = transform.GetComponentInChildren<PlayerAttackCtrl>();
    }

    public virtual void ChangeSkill(string skillName)
    {
        this.playerAttackCtrl.StopAllSkillType(SKILL_TYPE.DEFAULT);
        this.playerAttackCtrl.ChangeSkill(skillName);
        this.Attack();
    }

    public virtual void NextSkill(int idx)
    {
        this.playerAttackCtrl.NextSkill(idx);
        this.Attack();
    }

    public virtual void Attack()
    {
        this.playerAttackCtrl.Attack();
    }

    public virtual void SpecialAttack(string skillName)
    {
        this.playerAttackCtrl.Attack(skillName);
    }

    public virtual void TurnRight()
    {
        this.playerPlaneCtrl.planeAnimator.TurnRight();
    }

    public virtual void TurnLeft()
    {
        this.playerPlaneCtrl.planeAnimator.TurnLeft();
    }

    public virtual void Idle()
    {
        this.playerPlaneCtrl.planeAnimator.Idle();
    }

}
