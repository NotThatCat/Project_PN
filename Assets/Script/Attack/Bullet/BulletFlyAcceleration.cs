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
            if (this.currentSpeed > this.maxSpeed)
            {
                return;
            }
        }

        if (this.acceleration < 0)
        {
            if (this.currentSpeed < this.maxSpeed)
            {
                this.MaxSpeedEffect();
                return;
            }
        }
        this.currentSpeed = this.currentSpeed + this.acceleration * Time.deltaTime;
    }

    protected virtual void MaxSpeedEffect()
    {
        this.currentSpeed = this.maxSpeed;
        this.bulletCtrl.despawn.Despawning();
    }
}
