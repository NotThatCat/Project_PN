using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackCtrl : AttackCtrl
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    //[SerializeField] protected BossData bossData;


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

    protected override int GetDefaultSkill()
    {
        return 0;
    }
}
