using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawn : Despawn
{

    [SerializeField] protected string effectName = "ExploreWhite";

    /// <summary>
    /// Need to Instantiate any effect after despawn?
    /// </summary>
    protected override void AfterDespawan()
    {
        EffectManager.instance.Spawn(effectName, transform.position);
    }
}
