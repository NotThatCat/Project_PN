using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : PMonoBehaviour
{
    [SerializeField] public EnemyMoving enemyMoving;
    [SerializeField] public Despawn despawn;
    [SerializeField] public Level level;
    [SerializeField] public EnemyData enemyData;
    [SerializeField] public EnemyAttackCtrl enemyAttackCtrl;

    #region LoadData
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyMoving();
        this.LoadDespawn();
        this.LoadLevel();
        this.LoadEnemyData();
        this.LoadEnemyAttackCtrl();
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

    protected virtual void LoadEnemyData()
    {

        if (this.enemyData != null) return;
        string resPath = "Enemy/" + transform.name;
        this.enemyData = Resources.Load<EnemyData>(resPath);
    }

    protected virtual void LoadEnemyAttackCtrl()
    {
        this.enemyAttackCtrl = transform.GetComponentInChildren<EnemyAttackCtrl>();
    }

    #endregion

    protected override void Awake()
    {

    }

    public virtual List<DropRate> GetDrop()
    {
        if(enemyData == null) return null;
        if(enemyData.dropList != null) return enemyData.dropList;
        return null;
    }

    public virtual void StartDefaultAttack()
    {
        this.enemyAttackCtrl.DefaultAttack();
    }
}
