using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAcceleration : MoveByPath
{
    [Header("Acceleration")]
    [SerializeField] public float baseMovingSpeed = 3f;
    [SerializeField] public List<AccelerationData> acceleration;

    protected override void Update()
    {
        if (this.startMoving)
        {
            this.MoveToNextCheckPoint();
            this.Moving();
        }
    }

    protected override void Moving()
    {
        if (this.checkpointIndex < this.acceleration.Count)
        {
            this.movingSpeed = this.CaculateAccSpeed();
        }

        float step = this.movingSpeed * Time.deltaTime;
        transform.parent.position = Vector3.MoveTowards(
            transform.parent.position,
            this.CurrentCheckPoint().position,
            step);
    }

    protected virtual float CaculateAccSpeed()
    {
        AccelerationData currentAcc = this.acceleration[checkpointIndex];
        float newSpeed = currentAcc.startSpeed;
        if (currentAcc.useAcceleration)
        {
            newSpeed = newSpeed + currentAcc.acceleration * Time.deltaTime;
            newSpeed = this.CheckMaxSpeed(newSpeed, currentAcc.acceleration, currentAcc.maxSpeed);
            currentAcc.startSpeed = newSpeed;
        }
        else
        {
            newSpeed = baseMovingSpeed;
        }

        return newSpeed;
    }

    public virtual float CheckMaxSpeed(float speed, float acceleration, float maxSpeed)
    {
        if (acceleration > 0)
        {
            if (speed > maxSpeed)
            {
                return maxSpeed;
            }
        }

        if (acceleration < 0)
        {
            if (speed < maxSpeed)
            {
                return maxSpeed;
            }
        }

        return speed;
    }

    public override void ResetValue()
    {
        base.ResetValue();
        this.baseMovingSpeed = this.enemyCtrl.enemyData.baseMovingSpeed;
        this.acceleration = this.enemyCtrl.enemyData.acceleration;
    }
}
