using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : PMonoBehaviour
{
    public ItemSender itemSender;
    public Despawn despawn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.itemSender = transform.GetComponentInChildren<ItemSender>();
        this.despawn = transform.GetComponentInChildren<Despawn>();
    }
}
