using UnityEngine;

public class DamageSender : PMonoBehaviour
{
    [Header("Damage Sender")]
    [SerializeField] public float damage = 1;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        DamageReceiver damageReceiver = other.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        damageReceiver.Damaged(this.damage);
        AfterDamage();

        //other.SendMessage("OnDamaged", this);

    }


    /// <summary>
    /// Overide for other Damage style and effect
    /// </summary>
    protected virtual void AfterDamage()
    {
        this.Despawn();
    }

    protected virtual void Despawn()
    {
        Destroy(transform.parent.gameObject);
    }
}
