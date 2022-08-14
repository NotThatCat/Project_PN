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
}
