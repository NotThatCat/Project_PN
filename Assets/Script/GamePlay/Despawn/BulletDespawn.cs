using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawn : Despawn
{
    /// <summary>
    /// <summary>
    /// Despawn function, return Bullet Despawn to pool
    /// </summary>
    protected override void DespawnNow()
    {
        BulletSpawner.Instance.Despawn(transform.parent);
    }

}
