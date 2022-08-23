using UnityEngine;

public class DamageReceiver : PMonoBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] public float hp = 0;
    [SerializeField] public float maxHp = 1;
    [SerializeField] protected BULLET_SOURCEDAMAGE takeDamageFrom = BULLET_SOURCEDAMAGE.PLAYER;

    protected override void OnEnable()
    {
        this.ResetHP();
    }

    protected virtual void ResetHP()
    {
        this.hp = this.maxHp;
    }

    /// <summary>
    /// Perform Damage without condition
    /// </summary>
    /// <param name="damage"></param>
    /// <returns>If damage success</returns>
    public virtual bool Damaged(float damage)
    {
        this.TakeDamagedEffect();
        this.DecreaseHP(damage);
        if (this.hp <= 0) this.hp = 0;

        this.Dying();
        return true;
    }

    /// <summary>
    /// Perform Damage form takeDamageFrom
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="sourceDamage"></param>
    /// <returns>If damage success</returns>
    public virtual bool Damaged(float damage, BULLET_SOURCEDAMAGE sourceDamage)
    {
        if (sourceDamage == this.takeDamageFrom)
        {
            this.DecreaseHP(damage);
            this.TakeDamagedEffect();
            if (this.hp <= 0) this.hp = 0;

            this.Dying();
            return true;
        }
        return false;
    }

    protected virtual void DecreaseHP(float value)
    {
        this.hp -= value;
    }

    /// <summary>
    /// Overide for Phase 2+ Enemy, revive after death
    /// </summary>
    protected virtual void Dying()
    {
        if (this.IsAlive()) return;
        this.Death();
    }

    protected virtual bool IsAlive()
    {
        return this.hp > 0;
    }

    protected virtual void Death()
    {
        this.Despawn();
    }

    protected virtual void Despawn()
    {
        Destroy(transform.parent.gameObject);
    }

    protected virtual void TakeDamagedEffect()
    {

    }

}
