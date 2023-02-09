using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    [SerializeField] protected string effectName = "ExploreWhite";

    protected override void Despawn()
    {
        EffectManager.instance.Spawn(effectName, transform.position);
        base.Despawn();
    }

    protected override void TakeDamagedEffect()
    {
        this.UpdateUI();
    }

    protected virtual void UpdateUI()
    {
        float barValue = this.hp / this.maxHp;
        UIManager.instance.UpdatePlayerHPBar(barValue);
    }

    protected virtual void PlayerDeath()
    {
        GameManager.instance.PlayerDeath();
    }

    protected override void Death()
    {
        this.PlayerDeath();
        transform.parent.gameObject.SetActive(false);
    }
}
