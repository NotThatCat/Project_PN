using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCtrl : AttackCtrl
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected bool isTesting = false;

    //Testing
    [SerializeField] protected SkillCtrl testSkill;

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
        if (isTesting)
        {
            return testSkill.StartAttack();
        }
        else
        {
            return base.Attack();
        }
    }
}
