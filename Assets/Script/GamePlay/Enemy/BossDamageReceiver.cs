using UnityEngine;

public class BossDamageReceiver : EnemyDamageReceiver
{
    protected override void TakeDamagedEffect()
    {
        base.TakeDamagedEffect();
        UIManager.instance.UpdateBossHPBar(this.hp / this.maxHp);
    }
}
