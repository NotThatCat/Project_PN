using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDespawn : EnemyDespawn
{

    /// <summary>
    /// Need to Instantiate any effect after despawn?
    /// </summary>
    protected override void AfterDespawan()
    {
        base.AfterDespawan();
        UIManager.instance.DeactiveBossHPBar();
    }

}
