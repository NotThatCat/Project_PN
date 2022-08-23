using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    [SerializeField] protected EnemyCtrl enemyCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        this.enemyCtrl = transform.GetComponentInParent<EnemyCtrl>();
    }

    protected override void Despawn()
    {
        this.enemyCtrl.despawn.Despawning();
    }
}
