using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEM1_3 : SkillCtrl
{
    //
    [SerializeField] protected float bulletRotaFixed = 0.05f;
    protected override void Attack()
    {
        if (this.strikePoints.Count <= 0)
        {
            this.LogError("In FixedAttack: strikePoints not found");
            return;
        }

        float centerIndex = this.strikePoints.Count / 2;
        for (int i = 0; i < this.strikePoints.Count; i++)
        {
            Quaternion bulletRotation = new Quaternion(this.transform.rotation.x,
                this.transform.rotation.y,
                this.transform.rotation.z - this.bulletRotaFixed * (i - centerIndex),
                this.transform.rotation.w).normalized;

            this.SpawnBullet(this.strikePoints[i].position, bulletRotation);
        }
    }
}
