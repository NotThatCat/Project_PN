using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCtrl : PMonoBehaviour
{
    [SerializeField] protected SkillSO skillSO;
    [SerializeField] protected SkillManager skillManager;

    [SerializeField] protected string strikePointName = "StrikePoints";
    [SerializeField] protected List<Transform> strikePoints;

    [SerializeField] protected AttackCtrl attackCtrl;

    [Header("Attack Status")] // Require reset if Stop Attack
    [SerializeField] protected ATTACK_STATUS attackStatus = ATTACK_STATUS.READY_ATTACK;
    [SerializeField] protected bool attackInProgress = false;

    [SerializeField] protected bool playSound = false;

    [SerializeField] protected float finalDelay = 0f;
    [SerializeField] protected float finalCoolDown = 0f;

    [SerializeField] protected float cooldownLeft = 0f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(this.attackStatus == ATTACK_STATUS.IN_COOLDOWN)
        {
            this.ClockCooldownLeft();
        }
    }

    protected virtual void ClockCooldownLeft()
    {
        this.cooldownLeft -= Time.deltaTime;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlanCtrl();
        this.LoadSkillManager();
        this.LoadSkillData();
        this.LoadStrikePoint();
    }

    protected virtual void LoadPlanCtrl()
    {
        this.attackCtrl = transform.GetComponentInParent<AttackCtrl>();
    }

    protected virtual void LoadSkillManager()
    {
        this.skillManager = GameObject.Find("SkillManager").GetComponent<SkillManager>();
    }

    protected virtual void LoadSkillData()
    {
        if (this.skillManager == null) return;
        this.skillSO = this.skillManager.GetSkillData(transform.name);
    }

    /// <summary>
    /// For Overide Load StrikePoints
    /// </summary>
    protected virtual void LoadStrikePoint()
    {
        this.LoadStrikePointByName();
    }

    /// <summary>
    /// StrikePoints Already defined as a child
    /// </summary>
    protected virtual void LoadStrikePointByName()
    {
        if (this.strikePoints != null || this.strikePoints.Count != 0)
        {
            this.strikePoints = new List<Transform>();
        }

        Transform strikePointList = transform.Find(strikePointName);
        if (strikePointList)
        {
            foreach (Transform strikePoint in strikePointList)
            {
                this.strikePoints.Add(strikePoint);
            }
        }
    }

    public virtual Sprite GetSkillImage()
    {
        return skillSO.image;
    }

    public virtual bool StartAttack()
    {
        if (!attackInProgress && this.attackStatus != ATTACK_STATUS.DISABLE)
        {
            StartCoroutine(this.AttackProcess(this.skillSO.loopAttack));
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Currently Stop attack will reset all current attack process, which ignore all cooldown. 
    /// This is very bad (Need to improve in the feature)
    /// </summary>
    public virtual void StopAttack()
    {
        StopAllCoroutines();
        this.attackInProgress = false;
        attackStatus = ATTACK_STATUS.READY_ATTACK;
    }

    public virtual void StopSkillType(SKILL_TYPE skillType)
    {
        if(this.skillSO.skillType == skillType) this.StopAttack();
    }

    public virtual void DisableAttack()
    {
        StopAllCoroutines();
        attackStatus = ATTACK_STATUS.DISABLE;
    }

    public virtual void EnableAttack()
    {
        attackStatus = ATTACK_STATUS.READY_ATTACK;
    }

    public virtual float GetDelay()
    {
        this.CaculateFinalDelay();
        return this.finalDelay;
    }

    protected virtual IEnumerator AttackProcess(bool loop)
    {
        this.attackInProgress = true;

        do
        {
            ////This stop function by using attackStatus is not complete
            ////For any break to stop attack
            //if (this.attackStatus == ATTACK_STATUS.STOPING) break;

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
                yield return new WaitForSeconds(this.finalCoolDown);
                this.AfterCoolDown();
            }
        }
        while (loop);

        this.attackInProgress = false;
    }

    protected virtual void MainAttack()
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
                Quaternion bulletRotation = CaculateBulletRotationAtIndex(bulletTransform, i, centerIndex);
                this.SpawnBullet(bulletTransform.position, bulletRotation);
            }
        }
        else
        {
            this.SpawnBullet(bulletTransform.position, bulletTransform.rotation);
        }
    }

    protected virtual Quaternion CaculateBulletRotationAtIndex(Transform bulletTransform, int index, float centerIndex)
    {
        return new Quaternion(bulletTransform.rotation.x,
                        bulletTransform.rotation.y,
                        bulletTransform.rotation.z - this.skillSO.bulletRotaFixed * (index - centerIndex),
                        bulletTransform.rotation.w).normalized;
    }

    //Attact Process
    protected virtual void BeforeDelay()
    {
        this.BeforeDelayEffect();
        this.CaculateFinalDelay();
        this.attackStatus = ATTACK_STATUS.IN_DELAY;
    }
    protected virtual void AfterDelay()
    {
        this.AfterDelayEffect();
    }
    protected virtual void BeforeAttack()
    {
        this.attackStatus = ATTACK_STATUS.ATTACKING;
        this.BeforeAttackEffect();
    }
    protected virtual void AfterAttack()
    {
        this.attackStatus = ATTACK_STATUS.ATTACKED;
        this.AfterAttackEffect();
    }
    protected virtual void BeforeCoolDown()
    {
        this.BeforeCoolDownEffect();
        this.CaculateFinalCoolDown();
        this.ClockCoolDown();
        this.attackStatus = ATTACK_STATUS.IN_COOLDOWN;
    }
    protected virtual void AfterCoolDown()
    {
        this.attackStatus = ATTACK_STATUS.READY_ATTACK;
        this.AfterCoolDownEffect();
    }

    //Effect
    protected virtual void BeforeDelayEffect()
    {

    }
    protected virtual void AfterDelayEffect()
    {

    }
    protected virtual void BeforeAttackEffect()
    {

    }
    protected virtual void OnAttackEffect()
    {
        if (playSound) this.PlaySound();
    }
    protected virtual void AfterAttackEffect()
    {

    }
    protected virtual void BeforeCoolDownEffect()
    {

    }
    protected virtual void AfterCoolDownEffect()
    {

    }

    protected virtual void ClockCoolDown()
    {
        this.cooldownLeft = this.finalCoolDown;
    }

    protected virtual void CaculateFinalDelay()
    {
        if (this.skillSO.delayScaleWithLevel)
        {
            this.finalDelay = this.skillSO.baseDelay - ((this.skillSO.maxDelay - this.skillSO.minDelay) / (this.attackCtrl.GetMaxLevel())) * (this.attackCtrl.GetCurrentLevel());
        }
        else
        {
            this.finalDelay = this.skillSO.baseDelay;
        }

        if (this.finalDelay > this.skillSO.maxDelay) this.finalDelay = this.skillSO.maxDelay;
        if (this.finalDelay < this.skillSO.minDelay) this.finalDelay = this.skillSO.minDelay;
    }

    protected virtual void CaculateFinalCoolDown()
    {
        if (this.skillSO.isRandomCoolDown)
        {
            this.finalCoolDown = Random.Range(this.skillSO.minRanCoolDown, this.skillSO.maxRanCoolDown);
            return;
        }

        if (this.skillSO.coolDownScaleWithLevel)
        {
            this.finalCoolDown = this.skillSO.baseCoolDown - ((this.skillSO.maxCoolDown - this.skillSO.minCoolDown) / (this.attackCtrl.GetMaxLevel())) * (this.attackCtrl.GetCurrentLevel());
        }
        else
        {
            this.finalCoolDown = this.skillSO.baseCoolDown;
        }

        if (this.finalCoolDown > this.skillSO.maxCoolDown) this.finalCoolDown = this.skillSO.maxCoolDown;
        if (this.finalCoolDown < this.skillSO.minCoolDown) this.finalCoolDown = this.skillSO.minCoolDown;
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

    /// <summary>
    /// Get Damage 
    /// </summary>
    /// <returns></returns>
    protected virtual float GetDamage()
    {
        return this.skillSO.damage;
    }

    protected override void LogError(string error)
    {
        base.LogError(error);
        Debug.LogError(transform.name + " " + error);
    }

    protected virtual void PlaySound()
    {
        SoundManager.instance.PlaySFX(this.skillSO.soundID);
    }

    public override void ResetValue()
    {
        base.ResetValue();
        this.StopAttack();
    }
}
