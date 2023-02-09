using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : PMonoBehaviour
{
    [SerializeField] protected BulletCtrl bulletCtrl;
    [SerializeField] public float speed = 5f;
    [SerializeField] protected bool stopFly = false;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletCtrl();
    }
    protected virtual void LoadBulletCtrl()
    {
        this.bulletCtrl = transform.GetComponentInParent<BulletCtrl>();
    }

    protected override void Update()
    {
        if (!this.stopFly)
        {
            this.Fly();
        }
    }

    protected virtual void Fly()
    {
        transform.parent.Translate(Vector3.up * this.speed * Time.deltaTime);
    }
}
