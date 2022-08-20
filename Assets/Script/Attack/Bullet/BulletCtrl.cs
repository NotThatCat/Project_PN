using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : PMonoBehaviour
{
    [SerializeField] public Despawn despawn;
    [SerializeField] public BulletDamageSender bulletDamageSender;

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
