using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCtrl : AttackCtrl
{
    [SerializeField] protected EnemyCtrl enemyCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        this.enemyCtrl = transform.GetComponentInParent<EnemyCtrl>();
    }

    public override int GetMaxLevel()
    {
        return this.enemyCtrl.GetMaxLevel();
    }

    public override int GetCurrentLevel()
    {
        return this.enemyCtrl.GetCurrentLevel();
    }
}
