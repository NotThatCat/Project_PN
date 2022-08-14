using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDamageReceiver : DamageReceiver
{
    [SerializeField] protected string effectName = "ExploreWhite";

    protected override void Despawn()
    {
        EffectManager.instance.Spawn(effectName, transform.position);
        base.Despawn();
    }
}
