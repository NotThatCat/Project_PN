using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected bool isFlashEffect = true;
    [SerializeField] protected ColoredFlash coloredFlash;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
        this.LoadFlash();
    }

    protected virtual void LoadEnemyCtrl()
    {
        this.enemyCtrl = transform.GetComponentInParent<EnemyCtrl>();
    }

    protected virtual void LoadFlash()
    {
        if (this.coloredFlash != null) return;
        this.coloredFlash = transform.GetComponentInChildren<ColoredFlash>();
    }

    protected override void Despawn()
    {
        this.enemyCtrl.despawn.Despawning();
    }

    protected override void TakeDamagedEffect()
    {
        if (!this.IsAlive()) return;
        if(this.coloredFlash != null && !isFlashEffect) return;
        this.coloredFlash.Flash();
        base.TakeDamagedEffect();
    }

    public override void ResetValue()
    {
        this.maxHp = this.enemyCtrl.enemyData.hp;
        base.ResetValue();
    }
}
