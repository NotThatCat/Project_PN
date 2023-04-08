using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByTime : Despawn
{
    public float timer = 0f;
    public float delay = 7f;

    protected override void FixedUpdate()
    {
        this.DespawnByTiming();
    }

    protected virtual void DespawnByTiming()
    {
        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.delay) return;
        this.timer = 0;

        this.Despawning();
    }

    //public override void ResetValue()
    //{
    //    this.timer = this.delay;
    //    base.ResetValue();
    //}

    //protected override void FixedUpdate()
    //{
    //    this.Timing();
    //    this.Despawning();
    //}

    //protected virtual void Timing()
    //{
    //    this.timer += Time.fixedDeltaTime;
    //}

    //protected override bool IsReadyToDespawn()
    //{
    //    if (this.timer < this.delay) return false;
    //    return true;
    //}

    protected override void DespawnNow()
    {
        EffectSpawner.Instance.Despawn(transform.parent);
    }

    public override void ResetValue()
    {
        base.ResetValue();
        this.timer = 0;
    }

}
