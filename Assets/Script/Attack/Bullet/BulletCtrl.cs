using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : PMonoBehaviour
{
    public Despawn despawn;
    public BulletDamageSender bulletDamageSender;

    protected override void Start()
    {
        this.LoadComponents();
    }

    protected override void OnEnable()
    {
        this.LoadComponents();
    }

    protected override void LoadComponents()
    {
        this.despawn = transform.GetComponentInChildren<Despawn>();
        this.bulletDamageSender = GetComponentInChildren<BulletDamageSender>();
    }
}
