using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByPath : EnemyMoving
{
    [Header("By Path")]
    [SerializeField] protected Transform checkpointPath;
    [SerializeField] protected bool pathFinish = false;
    [SerializeField] protected float checkpointDistance = Mathf.Infinity;
    [SerializeField] protected int checkpointIndex = 0;
    [SerializeField] protected List<Transform> checkpoints;

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

    protected virtual Transform CurrentCheckPoint()
    {
        return this.checkpoints[this.checkpointIndex];
    }

    protected virtual void MoveToNextCheckPoint()
    {
        this.checkpointDistance = Vector3.Distance(transform.parent.position, this.CurrentCheckPoint().position);
        if (this.checkpointDistance < 0) this.checkpointIndex = 0;
        if (this.checkpointDistance <= 0.1f) this.checkpointIndex++;
        if (this.checkpointIndex >= this.checkpoints.Count)
        {
            this.checkpointIndex = this.checkpoints.Count - 1;
            this.MovingCompleted();
            this.pathFinish = true;
            this.Despawn();
        }
    }

    public virtual void LoadCheckPoints(Transform path)
    {
        this.checkpointPath = path;
        this.checkpointIndex = 0;
        this.checkpoints = new List<Transform>();
        foreach (Transform checkpoint in this.checkpointPath)
        {
            this.checkpoints.Add(checkpoint);
        }
    }

    protected virtual void Despawn()
    {
        enemyCtrl.despawn.Despawning();
    }

    public override void ResetValue()
    {
        this.startMoving = false;
        this.checkpointIndex = 0;
        base.ResetValue();
    }
}
