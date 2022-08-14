using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearFire : SkillCtrl
{
    //[Header("Spear Control")]
    //[SerializeField] protected bool useSpearNumbers = true;
    //[SerializeField] protected float spearNumbers = 2;
    //[SerializeField] protected float bulletRotaFixed = 0.05f;

    protected override void Attack()
    {
        if (this.strikePoints.Count <= 0)
        {
            this.LogError("In FixedAttack: strikePoints not found");
            return;
        }

        //If we using the SpearNumber, bullet always spawn at the first strikePoint
        if (this.skillData.useSpearNumbers)
        {
            float centerIndex = (this.skillData.spearNumbers - 1) / 2;
            for (int i = 0; i < this.skillData.spearNumbers; i++)
            {
                Quaternion bulletRotation = new Quaternion(this.transform.rotation.x,
                this.transform.rotation.y,
                this.transform.rotation.z - this.skillData.bulletRotaFixed * (i - centerIndex),
                this.transform.rotation.w).normalized;

                this.SpawnBullet(this.strikePoints[0].position, bulletRotation);
            }
        }
        else
        {
            float centerIndex = (this.strikePoints.Count - 1) / 2;
            for (int i = 0; i < this.strikePoints.Count; i++)
            {
                Quaternion bulletRotation = new Quaternion(this.transform.rotation.x,
                this.transform.rotation.y,
                this.transform.rotation.z - this.skillData.bulletRotaFixed * (i - centerIndex),
                this.transform.rotation.w).normalized;

                this.SpawnBullet(this.strikePoints[i].position, bulletRotation);
            }
        }
    }
}
