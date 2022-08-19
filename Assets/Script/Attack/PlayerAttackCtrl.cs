using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCtrl : AttackCtrl
{
    [SerializeField] protected PlayerCtrl playerCtrl;

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
        return this.playerCtrl.GetMaxLevel();
    }

    public override int GetCurrentLevel()
    {
        return this.playerCtrl.GetCurrentLevel();
    }
}