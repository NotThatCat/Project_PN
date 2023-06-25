using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class EnemyMoving : PMonoBehaviour
{
    [SerializeField] protected EnemyCtrl enemyCtrl;

    [Header("Moving")]
    [SerializeField] public MOVING_TYPE type = MOVING_TYPE.PATH;
    [SerializeField] public float movingSpeed = 2f;
    [SerializeField] protected bool startMoving = false;
    [SerializeField] protected bool movingComplete = false;
    [SerializeField] protected Vector3 targetPosition;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = transform.parent.GetComponent<EnemyCtrl>();
    }

    protected override void Update()
    {
        if (this.startMoving && this.targetPosition != null)
        {
            this.Moving();
        }
    }

    protected virtual void SetTargetMoving(Vector3 pos)
    {
        this.targetPosition = pos;
    }

    protected virtual void Moving()
    {
        MovingToPosition(targetPosition);
    }

    protected virtual void MovingToPosition(Vector3 postion)
    {
        float step = this.movingSpeed * Time.deltaTime;
        transform.parent.position = Vector3.MoveTowards(transform.parent.position, postion, step);
    }

    protected virtual void MovingCompleted()
    {
        this.movingComplete = true;
        this.startMoving = false;
    }

    public virtual bool IsEndMoving()
    {
        return this.movingComplete;
    }

    public virtual void StartMoving()
    {
        this.startMoving = true;
    }

    public virtual void StopMoving()
    {
        this.startMoving = false;
    }

    public override void ResetValue()
    {
        base.ResetValue();
        this.movingSpeed = this.enemyCtrl.enemyData.speed;
    }
}
