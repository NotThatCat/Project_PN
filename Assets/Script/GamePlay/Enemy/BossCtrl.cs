using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : EnemyCtrl
{
    [SerializeField] protected DamageReceiver bossDamageReceiver;
    [SerializeField] protected BossData bossData;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossData();
        this.LoadDamageReceiver();
    }

    protected virtual void LoadDamageReceiver()
    {
        this.bossDamageReceiver = transform.GetComponentInChildren<DamageReceiver>();
    }

    protected override void Awake()
    {

    }

    protected override void Start()
    {
        UIManager.instance.ActiveBossHPBar(this.bossDamageReceiver.hp / this.bossDamageReceiver.maxHp);
    }

    protected virtual void LoadBossData()
    {
        this.bossData = this.enemyManager.GetBossData(transform.name);
    }

    internal BossData GetBossData()
    {
        return this.bossData;
    }
}
