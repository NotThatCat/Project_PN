using UnityEngine;

public class DamageReceiver : PMonoBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected float hp = 0;
    [SerializeField] protected float maxHp = 1;
    [SerializeField] protected string takeDamageFrom = "";

    protected override void OnEnable()
    {
        this.ResetHP();
    }

    public virtual float HP()
    {
        return this.hp;
    }

    protected virtual void ResetHP()
    {
        this.hp = this.MaxHp();
    }

    public virtual float MaxHp()
    {
        return this.maxHp;
    }

    /// <summary>
    /// Perform Damage without condition
    /// </summary>
    /// <param name="damage"></param>
    /// <returns>If damage success</returns>
    public virtual bool Damaged(float damage)
    {
        this.hp -= damage;
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
    public virtual bool Damaged(float damage, string sourceDamage)
    {
        if (sourceDamage == this.takeDamageFrom)
        {
            this.hp -= damage;
            if (this.hp <= 0) this.hp = 0;

            this.Dying();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Overide for Phase 2+ Enemy, revive after death
    /// </summary>
    protected virtual void Dying()
    {
        if (this.IsAlive()) return;
        this.Despawn();
    }

    protected virtual bool IsAlive()
    {
        return this.hp > 0;
    }

    protected virtual void Despawn()
    {
        Destroy(transform.parent.gameObject);
    }

}
