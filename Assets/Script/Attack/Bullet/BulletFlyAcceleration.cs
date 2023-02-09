using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlyAcceleration : BulletFly
{
    [SerializeField] public float acceleration = -3.5f;
    [SerializeField] public float maxSpeed = 0f;
    [SerializeField] protected bool maxSpeedEffect = true;

    protected override void Update()
    {
        this.UpdateSpeed();
        base.Update();
    }

    protected virtual void UpdateSpeed()
    {
        if(this.acceleration > 0)
        {
            if (this.speed > this.maxSpeed)
            {
                return;
            }
        }

        if (this.acceleration < 0)
        {
            if (this.speed < this.maxSpeed)
            {
                this.MaxSpeedEffect();
                return;
            }
        }
        this.speed = this.speed + this.acceleration * Time.deltaTime;
    }

    protected virtual void MaxSpeedEffect()
    {
        this.speed = this.maxSpeed;
        this.bulletCtrl.despawn.Despawning();
    }
}
