using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpItemSender : ItemSender
{
    [SerializeField] protected int amount = 1;

    protected override void ItemInteraction()
    {
        for(int i = 0; i < amount; i++)
        {
            GameManager.instance.PlayerLevelUp();
        }
        this.itemCtrl.despawn.Despawning();
    }
}
