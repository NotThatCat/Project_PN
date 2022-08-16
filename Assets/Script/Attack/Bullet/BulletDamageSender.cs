using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageSender : DamageSender
{
    [Header("Bullet")]
    [SerializeField] public BulletCtrl bulletCtrl;
    [SerializeField] protected BULLET_SOURCEDAMAGE sourceDamage = BULLET_SOURCEDAMAGE.ENEMY;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.AdjustSourceDamage();
        this.bulletCtrl = transform.parent.GetComponent<BulletCtrl>();
    }

    protected virtual void AdjustSourceDamage()
    {
        if (transform.parent.name.StartsWith("P"))
        {
            this.sourceDamage = BULLET_SOURCEDAMAGE.PLAYER;
        }
        if (transform.parent.name.StartsWith("E"))
        {
            this.sourceDamage = BULLET_SOURCEDAMAGE.ENEMY;
        }
        if (transform.parent.name.StartsWith("G"))
        {
            this.sourceDamage = BULLET_SOURCEDAMAGE.GLOBAL;
        }
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
