using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCtrlNoUse : PMonoBehaviour
{
    //    //[SerializeField] protected PlayerCtrl playerCtrl;
    //    [SerializeField] protected PlaneCtrl planeCtrl;
    //    [SerializeField] protected float attackDelay = 0.2f;
    //    [SerializeField] protected float fixedTimer = 0f;
    //    [SerializeField] protected float baseDelay = 0.2f;
    //    [SerializeField] protected float finalDelay = 0.2f;
    //    [SerializeField] protected float minDelay = 0.05f;
    //    [SerializeField] protected float delayPerLevel = 0.05f;
    //    [SerializeField] protected string bulletName = "DefaultBullet";
    //    [SerializeField] protected List<Transform> strikePoints;

    //    protected override void LoadComponents()
    //    {
    //        base.LoadComponents();
    //        this.LoadPlaneCtrl();
    //        this.LoadStrikePoint();
    //    }

    //    protected override void Start()
    //    {
    //        // this.playerCtrl = transform.parent.GetComponent<PlayerCtrl>();
    //    }

    //    protected override void FixedUpdate()
    //    {
    //        this.FixedAttacking();
    //    }

    //    protected virtual void Attack()
    //    {
    //        if (this.strikePoints.Count <= 0)
    //        {
    //            this.AttackWithNoStrikePoint();
    //            return;
    //        }

    //        this.AttackWithStrikePoint();
    //    }

    //    protected virtual void AttackWithNoStrikePoint()
    //    {
    //        Vector3 shootPosition = transform.position;
    //        this.SpawnBullet(shootPosition);
    //        // ScoreManager.instance.Add(ScoreType.BulletCount.ToString());
    //    }

    //    protected virtual void AttackWithStrikePoint()
    //    {
    //        foreach (Transform strikePoint in this.strikePoints)
    //        {
    //            Vector3 shootPosition = strikePoint.position;
    //            Quaternion rotation = strikePoint.rotation;
    //            this.SpawnBullet(shootPosition, rotation);
    //        }

    //        // ScoreManager.instance.Add(ScoreType.BulletCount.ToString());
    //    }

    //    protected virtual Transform SpawnBullet(Vector3 shootPosition, Quaternion rotation)
    //    {
    //        Transform newBullet = this.SpawnBullet(shootPosition);
    //        newBullet.rotation = rotation;
    //        return newBullet;
    //    }

    //    protected virtual Transform SpawnBullet(Vector3 shootPosition)
    //    {
    //        Transform newBullet = BulletManager.instance.Spawn(this.bulletName, shootPosition);
    //        BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
    //        if (bulletCtrl == null) Debug.LogError("Missing BulletCtrl in newBullet");
    //        newBullet.gameObject.SetActive(true);

    //        //Debug.LogError("newBullet created", newBullet.gameObject);

    //        BulletDamageSender damageSender = bulletCtrl.bulletDamageSender;
    //        if (damageSender == null) Debug.LogError("Bullet has no damage sender", bulletCtrl.gameObject);
    //        damageSender.damage = this.GetDamage();

    //        return newBullet;
    //    }

    //    /// <summary>
    //    /// Get Damage 
    //    /// </summary>
    //    /// <returns></returns>
    //    protected virtual float GetDamage()
    //    {
    //        // return this.playerCtrl.playerLevel.CurrentLevel();
    //        return 1;
    //    }

    //    protected virtual void FixedAttacking()
    //    {
    //        // if (GameOver.instance.IsGameOver()) return;

    //        this.fixedTimer += Time.fixedDeltaTime;
    //        if (this.fixedTimer < this.Delay()) return;
    //        this.fixedTimer = 0;

    //        this.LoadStrikePoint();
    //        this.Attack();
    //    }

    //    protected virtual float Delay()
    //    {
    //        //int level = this.playerCtrl.playerLevel.CurrentLevel();
    //        //this.finalDelay = this.baseDelay - (level * this.delayPerLevel);
    //        //if (this.finalDelay < this.minDelay) this.finalDelay = this.minDelay;
    //        return this.finalDelay;
    //    }

    //    protected virtual void LoadPlaneCtrl()
    //    {
    //        this.planeCtrl = transform.GetComponentInParent<PlaneCtrl>();
    //    }

    //    protected virtual void LoadStrikePoint()
    //    {
    //        //this.strikePoints = this.playerCtrl.playerModels.StrikePoints();
    //        this.strikePoints = this.planeCtrl.strikePoints;
    //    }
}
