using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawn : Despawn
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected string effectName = "ExploreWhite";
    [SerializeField] protected bool dropable = false;

    /// <summary>
    /// Need to Instantiate any effect after despawn?
    /// </summary>
    protected override void AfterDespawan()
    {
        this.EffectSpawn();
        this.ItemSpawn();
    }

    protected virtual void EffectSpawn()
    {
        Transform effectPrf = EffectSpawner.Instance.Spawn(effectName, transform.position, transform.rotation);
        effectPrf.gameObject.SetActive(true);
    }

    protected virtual void ItemSpawn()
    {
        List<DropRate> dropList = this.enemyCtrl.GetDrop();
        if (dropList == null) return;
        if (this.enemyCtrl.enemyData.canDrop && this.enemyCtrl.enemyData.dropList.Count > 0)
        {
            //ItemManager.Instance.Spawn(dropName, transform.parent.position);
            ItemSpawner.Instance.Drop(this.enemyCtrl.enemyData.dropList, transform.position, transform.rotation);
        }
    }

    protected override void LoadComponents()
    {
        this.LoadEnemyCtrl();
        base.LoadComponents();
    }

    protected virtual void LoadEnemyCtrl()
    {
        this.enemyCtrl = transform.GetComponentInParent<EnemyCtrl>();
    }

    protected override void DespawnNow()
    {
        EnemySpawner.Instance.Despawn(transform.parent);
    }
}
