using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawn : Despawn
{

    [SerializeField] protected string effectName = "ExploreWhite";
    [SerializeField] protected bool dropable = false;
    [SerializeField] protected string dropName = "None";

    /// <summary>
    /// Need to Instantiate any effect after despawn?
    /// </summary>
    protected override void AfterDespawan()
    {
        EffectManager.instance.Spawn(effectName, transform.position);

        if (this.dropable)
        {
            ItemManager.instance.Spawn(dropName, transform.parent.position);
        }
    }
}
