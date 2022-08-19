using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : PMonoBehaviour
{
    [SerializeField] protected EnemyManager enemyManager;
    [SerializeField] public EnemyMoving enemyMoving;
    [SerializeField] public Despawn despawn;
    [SerializeField] public Level level;
    [SerializeField] protected BossData bossData;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyManager();
        this.LoadEnemyMoving();
        this.LoadDespawn();
        this.LoadLevel();
        this.LoadBossData();
    }

    protected override void Awake()
    {

    }

    protected virtual void LoadEnemyManager()
    {
        this.enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        if (this.enemyManager == null)
        {
            Debug.Log("Cannot Find EnemyManager");
        }
    }

    protected virtual void LoadBossData()
    {
        this.bossData = this.enemyManager.GetBossData(transform.name);
    }

    internal BossData GetBossData()
    {
        return this.bossData;
    }

    protected virtual void LoadLevel()
    {
        this.level = transform.GetComponentInChildren<Level>();
    }

    protected virtual void LoadDespawn()
    {
        this.despawn = transform.GetComponentInChildren<Despawn>();
    }

    public virtual int GetCurrentLevel()
    {
        return this.level.level;
    }

    public virtual int GetMaxLevel()
    {
        return this.level.maxLevel;
    }

    protected virtual void LoadEnemyMoving()
    {
        this.enemyMoving = transform.GetComponentInChildren<EnemyMoving>();
    }
}
