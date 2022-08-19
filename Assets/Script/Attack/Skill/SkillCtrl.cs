using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCtrl : PMonoBehaviour
{
    [SerializeField] protected SkillData skillData;

    [SerializeField] protected string strikePointName = "StrikePoints";
    [SerializeField] protected string strikePointTemplate = "defbullet";
    [SerializeField] protected List<Transform> strikePoints;

    [SerializeField] protected AttackCtrl attackCtrl;

    [Header("Attack Status")] // Require reset if Stop Attack
    [SerializeField] protected ATTACK_STATUS attackStatus = ATTACK_STATUS.READY_ATTACK;
    [SerializeField] protected bool loopAttack = true;
    [SerializeField] protected bool attackInProgress = false;

    protected float fixedTimer = 0f;

    // ******* Attack Process:
    // isAttack
    // isreadyToAttack?
    // Delay caculate -> FinalDelay
    // Perform Attack
    // Cooldown -> FinalCooldown
    // ReadyToAttack
    // *******


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlanCtrl();
        this.LoadSkillData();
        this.LoadStrikePoint();
    }

    protected virtual void LoadPlanCtrl()
    {
        this.attackCtrl = transform.GetComponentInParent<AttackCtrl>();
    }

    protected virtual void LoadSkillData()
    {

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

    public virtual bool StartAttack()
    {
        if (!attackInProgress && this.attackStatus != ATTACK_STATUS.DISABLE)
        {
            StartCoroutine(this.AttackProcess(this.loopAttack));
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

    public virtual void DisableAttack()
    {
        StopAllCoroutines();
        attackStatus = ATTACK_STATUS.DISABLE;
    }

    public virtual float GetDelay()
    {
        this.CaculateFinalDelay();
        return this.skillData.finalDelay;
    }

    // Attack Process:
    // isAttack
    // isreadyToAttack?
    // Delay caculate -> FinalDelay
    // FixedAttack
    // Cooldown -> FinalCooldown
    // ReadyToAttack
    protected virtual IEnumerator AttackProcess(bool loop)
    {
        this.attackInProgress = true;

        //Perform attack once before loop (but how to prevent duplicate code?)
        this.CaculateFinalDelay();
        if (this.skillData.finalDelay > 0)
        {
            this.attackStatus = ATTACK_STATUS.IN_DELAY;
            yield return new WaitForSeconds(this.skillData.finalDelay);
        }

        this.attackStatus = ATTACK_STATUS.ATTACKING;
        this.Attack();
        this.attackStatus = ATTACK_STATUS.ATTACKED;

        this.CaculateFinalCoolDown();
        if (this.skillData.finalCoolDown > 0)
        {
            this.attackStatus = ATTACK_STATUS.IN_COOLDOWN;
            yield return new WaitForSeconds(this.skillData.finalCoolDown);
            this.attackStatus = ATTACK_STATUS.READY_ATTACK;
        }

        while (loop)
        {
            ////This stop function by using attackStatus is not complete
            ////For any break to stop attack
            //if (this.attackStatus == ATTACK_STATUS.STOPING) break;

            this.CaculateFinalDelay();
            if (this.skillData.finalDelay > 0)
            {
                this.attackStatus = ATTACK_STATUS.IN_DELAY;
                yield return new WaitForSeconds(this.skillData.finalDelay);
            }

            this.attackStatus = ATTACK_STATUS.ATTACKING;
            this.Attack();
            this.attackStatus = ATTACK_STATUS.ATTACKED;

            this.CaculateFinalCoolDown();
            if (this.skillData.finalCoolDown > 0)
            {
                this.attackStatus = ATTACK_STATUS.IN_COOLDOWN;
                yield return new WaitForSeconds(this.skillData.finalCoolDown);
                this.attackStatus = ATTACK_STATUS.READY_ATTACK;
            }
        }

        this.attackInProgress = false;
    }

    protected virtual void CaculateFinalDelay()
    {
        //this.skillData.finalDelay = this.skillData.baseDelay;
        this.skillData.finalDelay = this.skillData.baseDelay - ((this.skillData.maxDelay - this.skillData.minDelay) / (this.attackCtrl.GetMaxLevel() - 1)) * (this.attackCtrl.GetCurrentLevel() - 1);

        if (this.skillData.finalDelay > this.skillData.maxDelay) this.skillData.finalDelay = this.skillData.maxDelay;
        if (this.skillData.finalDelay < this.skillData.minDelay) this.skillData.finalDelay = this.skillData.minDelay;
    }

    protected virtual void CaculateFinalCoolDown()
    {
        //this.skillData.finalCoolDown = this.skillData.baseCoolDown;
        this.skillData.finalCoolDown = this.skillData.baseCoolDown - ((this.skillData.maxCoolDown - this.skillData.minCoolDown) / (this.attackCtrl.GetMaxLevel() - 1)) * (this.attackCtrl.GetCurrentLevel() - 1);

        if (this.skillData.finalCoolDown > this.skillData.maxCoolDown) this.skillData.finalCoolDown = this.skillData.maxCoolDown;
        if (this.skillData.finalCoolDown < this.skillData.minCoolDown) this.skillData.finalCoolDown = this.skillData.minCoolDown;
    }

    protected virtual void Attack()
    {
        if (this.strikePoints.Count <= 0)
        {
            this.LogError("In FixedAttack: strikePoints not found");
            return;
        }

        foreach (Transform sp in this.strikePoints)
        {
            this.SpawnBullet(sp.position, this.transform.rotation);
        }
    }

    protected virtual Transform SpawnBullet(Vector3 shootPosition, Quaternion rotation)
    {
        Transform newBullet = BulletManager.instance.Spawn(this.skillData.bulletName, shootPosition, rotation);
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
        return this.skillData.damage;
    }

    protected override void LogError(string error)
    {
        base.LogError(error);
        Debug.LogError(transform.name + " " + error);
    }
}
