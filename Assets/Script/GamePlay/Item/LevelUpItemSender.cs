using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpItemSender : ItemSender
{
    protected override void ItemInteraction()
    {
        GameManager.instance.PlayerLevelUp();
        this.itemCtrl.despawn.Despawning();
    }
}
