using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCtrl : Skill
{
    [SerializeField] protected int currentStack = 0;
    [SerializeField] private bool startNextStack = false;


    protected virtual void StackableClock()
    {
        if (this.currentStack == this.skillSO.maxStack) return;

        this.cooldownLeft -= Time.deltaTime;
        if (this.cooldownLeft < 0f)
        {
            this.ResetStack();
        }
        this.OnCoolDownNotify();
    }

    protected virtual void ResetStack()
    {
        this.cooldownLeft = 0f;
        this.currentStack++;
        if (this.currentStack < this.skillSO.maxStack)
        {
            this.cooldownLeft = this.finalCoolDown;
        }
    }

    protected override void LoadSkillData()
    {
        if (this.skillManager == null) return;
        this.skillSO = this.skillManager.GetSkillData(transform.name);

        if (this.skillSO == null) return;
        if (this.skillSO.stackAble)
        {
            this.finalCoolDown = this.skillSO.maxCoolDown;
            this.currentStack = this.skillSO.maxStack;
        }
    }

    public override Sprite GetSkillImage()
    {
        return skillSO.image;
    }

    public override bool StartAttack()
    {
        if (this.attackStatus == ATTACK_STATUS.DISABLE) return false;

        if (attackInProgress)
        {
            if (this.skillSO.stackAble && this.currentStack > 0 && !this.startNextStack)
            {
                this.currentStack--;
                this.startNextStack = true;
                StartCoroutine(this.AttackProcess(this.skillSO.loopAttack));
                return true;
            }
            else return false;
        }
        else
        {
            if (this.skillSO.stackAble && this.currentStack > 0 && !this.startNextStack)
            {
                this.currentStack--;
            }
            if (this.attackStatus == ATTACK_STATUS.READY_ATTACK) StartCoroutine(this.AttackProcess(this.skillSO.loopAttack));
            return true;
        }
    }

    protected override IEnumerator AttackProcess(bool loop)
    {
        this.attackInProgress = true;

        do
        {
            this.startNextStack = false;

            //////////////// Delay
            if (this.skillSO.useDelay)
            {
                this.BeforeDelay();
                yield return new WaitForSeconds(this.finalDelay);
                this.AfterDelay();
            }

            //////////////// Attack
            this.BeforeAttack();

            if (this.skillSO.isBurstAttack)
            {
                for (int i = 0; i < this.skillSO.burstNumber; i++)
                {
                    this.OnAttackEffect();
                    this.MainAttack();
                    if (this.skillSO.useBaseDelay)
                    {
                        yield return new WaitForSeconds(this.finalDelay);
                    }
                    yield return new WaitForSeconds(this.skillSO.burstDelay);
                }
            }
            else
            {
                this.OnAttackEffect();
                this.MainAttack();
            }

            this.AfterAttack();

            //////////////// Cool Down
            if (this.skillSO.useCoolDown)
            {
                this.BeforeCoolDown();
                if (!this.skillSO.stackAble) yield return new WaitForSeconds(this.finalCoolDown);
                this.AfterCoolDown();
            }
        }
        while (loop || this.startNextStack);

        this.attackInProgress = false;
    }

    protected override void MainAttack()
    {
        Transform attackTransform = this.transform;
        if (this.strikePoints.Count <= 0)
        {
            this.GenerateBulletAtTransform(attackTransform);
        }
        else
        {
            foreach (Transform sp in this.strikePoints)
            {
                attackTransform = sp;
                this.GenerateBulletAtTransform(attackTransform);
            }
        }
    }

    protected virtual void GenerateBulletAtTransform(Transform bulletTransform)
    {
        if (this.skillSO.isSpearAttack)
        {
            float centerIndex = (this.skillSO.spearNumbers - 1) / 2;
            for (int i = 0; i < this.skillSO.spearNumbers; i++)
            {
                Quaternion bulletRotation = CalculateBulletRotationAtIndex(bulletTransform, i, centerIndex);
                this.SpawnBullet(bulletTransform.position, bulletRotation);
            }
        }
        else
        {
            this.SpawnBullet(bulletTransform.position, bulletTransform.rotation);
        }
    }

    protected virtual Transform SpawnBullet(Vector3 shootPosition, Quaternion rotation)
    {
        //Transform newBullet = BulletManager.instance.Spawn(this.skillSO.bulletName, shootPosition, rotation);
        //BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
        //if (bulletCtrl == null) this.LogError("Missing BulletCtrl in newBullet");
        //newBullet.gameObject.SetActive(true);

        //BulletDamageSender damageSender = bulletCtrl.bulletDamageSender;
        //if (damageSender == null) Debug.LogError("Bullet has no damage sender", bulletCtrl.gameObject);
        //damageSender.damage = this.GetDamage();

        //return newBullet;

        Transform newBullet = BulletSpawner.Instance.Spawn(this.skillSO.bulletName, shootPosition, rotation);
        BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
        if (bulletCtrl == null) this.LogError("Missing BulletCtrl in newBullet");
        newBullet.gameObject.SetActive(true);

        BulletDamageSender damageSender = bulletCtrl.bulletDamageSender;
        if (damageSender == null) Debug.LogError("Bullet has no damage sender", bulletCtrl.gameObject);
        damageSender.damage = this.GetDamage();

        return newBullet;
    }

    protected virtual Quaternion CalculateBulletRotationAtIndex(Transform bulletTransform, int index, float centerIndex)
    {
        return new Quaternion(bulletTransform.rotation.x,
                        bulletTransform.rotation.y,
                        bulletTransform.rotation.z - this.skillSO.bulletRotaFixed * (index - centerIndex),
                        bulletTransform.rotation.w).normalized;
    }


    protected override void BeforeCoolDown()
    {
        this.BeforeCoolDownEffect();
        this.CalculateFinalCoolDown();
        if (!this.skillSO.stackAble) this.UpdateCoolDownLeft();
        this.attackStatus = ATTACK_STATUS.IN_COOLDOWN;
    }

    protected virtual void UpdateCoolDownLeft()
    {
        this.cooldownLeft = this.finalCoolDown;
    }

}
