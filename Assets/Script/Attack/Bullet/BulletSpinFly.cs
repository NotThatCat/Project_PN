using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpinFly : BulletFly
{
    [SerializeField] protected float rotateSpeed = 1500f;
    [SerializeField] protected Transform model;
    [SerializeField] protected Transform damageSender;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRotateModel();
    }

    protected virtual void LoadRotateModel()
    {
        this.model = transform.parent.Find("Model");
        this.damageSender = transform.parent.Find("BulletDamageSender");
    }

    protected override void Update()
    {
        transform.parent.Translate(Vector3.up * this.speed * Time.deltaTime);
        this.rotate();
    }

    protected virtual void rotate()
    {
        this.model.Rotate(new Vector3(0, 0, 1), rotateSpeed * Time.deltaTime, Space.Self);
        this.damageSender.Rotate(new Vector3(0, 0, 1), rotateSpeed * Time.deltaTime, Space.Self);
    }
}
