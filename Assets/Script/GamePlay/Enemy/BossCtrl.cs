using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : EnemyCtrl
{
    [SerializeField] protected DamageReceiver bossDamageReceiver;

    protected override void LoadComponents()
    {
        base.LoadComponents();
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

    public override void ResetValue()
    {
        base.ResetValue();
        this.ActiveBossHPBar();
    }

    protected virtual void ActiveBossHPBar()
    {
        UIManager.instance.ActiveBossHPBar(this.bossDamageReceiver.hp / this.bossDamageReceiver.maxHp);
    }
}
