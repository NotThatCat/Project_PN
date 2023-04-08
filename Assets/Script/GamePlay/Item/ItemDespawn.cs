using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDespawn : Despawn
{

    [SerializeField] protected string effectName = "ExploreWhite";

    /// <summary>
    /// Need to Instantiate any effect after despawn?
    /// </summary>
    protected override void AfterDespawan()
    {
        EffectSpawner.Instance.Spawn(effectName, transform.position);
    }
}
