using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSender : PMonoBehaviour
{
    [SerializeField] protected ItemCtrl itemCtrl;
    [SerializeField] protected string effectName = "ItemReceive";

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;

        ItemReceiver itemReceiver = other.GetComponent<ItemReceiver>();
        if (itemReceiver == null) return;

        itemReceiver.GetItemEffect(effectName);
        this.ItemInteraction();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.itemCtrl = transform.GetComponentInParent<ItemCtrl>();
    }

    protected abstract void ItemInteraction();
}
