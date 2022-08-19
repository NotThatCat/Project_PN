using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoving : MoveByPath
{

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

        float step = this.movingSpeed * Time.deltaTime;
        transform.parent.position = Vector3.MoveTowards(
            transform.parent.position,
            this.CurrentCheckPoint().position,
            step);
    }

    protected override void MoveToNextCheckPoint()
    {
        this.checkpointDistance = Vector3.Distance(transform.parent.position, this.CurrentCheckPoint().position);
    }

    public virtual void MovingToCheckPoint(int idx)
    {
        this.checkpointIndex = idx;
    }

    public virtual void MovingToPosition(Transform newTransform)
    {
        if (this.checkpoints == null || this.checkpoints.Count == 0)
        {
            this.checkpoints = new List<Transform>();
        }
        this.checkpoints.Add(newTransform);
        this.checkpointIndex = this.checkpoints.Count - 1;
    }
    
    public virtual void ChangeSpeed(float newSpeed)
    {
        this.movingSpeed = newSpeed;
    }
}
