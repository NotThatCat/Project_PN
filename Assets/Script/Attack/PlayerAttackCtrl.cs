using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAttackCtrl : AttackCtrl
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected string currentSpecialSkill;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        this.playerCtrl = transform.GetComponentInParent<PlayerCtrl>();
    }

    public override int GetMaxLevel()
    {
        int currentSkill = this.playerCtrl.GetMaxLevel();
        this.UpdateUICurrentSkill();
        return currentSkill;
    }

    public override int GetCurrentLevel()
    {
        int currentSkill = this.playerCtrl.GetCurrentLevel();
        this.UpdateUICurrentSkill();
        return currentSkill;
    }

    protected virtual void UpdateUICurrentSkill()
    {
        UIManager.instance.UpdatePlayerCurrentSkill(this.GetCurrentSkillImage());
    }

    //Testing
    public override bool Attack()
    {
        return base.Attack();
    }

    protected override int GetDefaultSkill()
    {
        return this.currentSkill;
        //return 0;
    }

    public virtual void RegisOnSkillCoolDown(string skillName, Action<float, float> onCoolDown)
    {
        this.skillCtrls[this.GetSkillIndexByName(skillName)].OnCoolDown += onCoolDown;
    }

    public virtual void UnRegisOnSkillCoolDown(string skillName, Action<float, float> onCoolDown)
    {
        this.skillCtrls[this.GetSkillIndexByName(skillName)].OnCoolDown -= onCoolDown;
    }
}
