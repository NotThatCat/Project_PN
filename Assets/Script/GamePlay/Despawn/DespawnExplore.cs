using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnExplore : Despawn
{
    [SerializeField] protected BulletCtrl bulletCtrl;
    [SerializeField] protected string childName;
    [SerializeField] protected int childNumber = 4;
    [SerializeField] protected float angle = 360;
    [SerializeField] protected float childDamage = 1;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletCtrl();
    }

    protected virtual void LoadBulletCtrl()
    {
        this.bulletCtrl = transform.GetComponentInParent<BulletCtrl>();
    }

    protected override void DespawnNow()
    {
        this.GenerateChild();
        base.DespawnNow();
    }

    protected virtual void GenerateChild()
    {
        float centerIndex = (float)(this.childNumber - 1) / 2;
        for (int i = 0; i < childNumber; i++)
        {
            Quaternion childRotation = CaculateChildRotationAtIndex(i, centerIndex);
            this.SpawnBullet(childRotation);
        }
    }

    protected virtual Quaternion CaculateChildRotationAtIndex(int index, float centerIndex)
    {
        float pos = (this.angle / this.childNumber) * (((float)index - centerIndex));

        Quaternion childRotation = transform.rotation;
        childRotation.eulerAngles = new Vector3(0, 0, pos);

        return childRotation;
    }

    protected virtual Transform SpawnBullet(Quaternion rotation)
    {
        Transform newBullet = BulletManager.instance.Spawn(childName, transform.position, rotation);
        BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
        if (bulletCtrl == null) this.LogError("Missing BulletCtrl in newBullet");
        newBullet.gameObject.SetActive(true);

        BulletDamageSender damageSender = bulletCtrl.bulletDamageSender;
        if (damageSender == null) Debug.LogError("Bullet has no damage sender", bulletCtrl.gameObject);
        damageSender.damage = this.childDamage;

        return newBullet;
    }
}
