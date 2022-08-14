using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageSender : DamageSender
{
    [Header("Bullet")]
    [SerializeField] public BulletCtrl bulletCtrl;
    [SerializeField] protected string sourceDamage = "Enemy";

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.bulletCtrl = transform.parent.GetComponent<BulletCtrl>();
    }

    protected override void Start()
    {
        this.bulletCtrl = transform.parent.GetComponent<BulletCtrl>();
    }

    protected override void Despawn()
    {
        this.bulletCtrl.despawn.Despawning();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        DamageReceiver damageReceiver = other.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        if (damageReceiver.Damaged(this.damage, this.sourceDamage))
        {
            AfterDamage();
        }
    }
}
