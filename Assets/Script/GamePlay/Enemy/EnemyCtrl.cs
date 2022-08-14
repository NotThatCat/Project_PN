using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : PMonoBehaviour
{
    [SerializeField] public EnemyModelCtrl enemyModelCtrl;
    [SerializeField] public EnemyMoving enemyMoving;
    [SerializeField] public Despawn despawn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyModelCtrl();
        this.LoadEnemyMoving();
        this.LoadDespawn();
    }

    protected virtual void LoadDespawn()
    {
        this.despawn = transform.GetComponentInChildren<Despawn>();
    }

    protected virtual void LoadEnemyModelCtrl()
    {
        this.enemyModelCtrl = transform.GetComponentInChildren<EnemyModelCtrl>();
    }

    protected virtual void LoadEnemyMoving()
    {
        this.enemyMoving = transform.GetComponentInChildren<EnemyMoving>();
    }
}
