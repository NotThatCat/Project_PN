using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCtrl : PMonoBehaviour
{
    [SerializeField] public ItemData itemData;
    [SerializeField] public ItemSender itemSender;
    [SerializeField] public Despawn despawn;
    [SerializeField] public SpriteRenderer itemModel;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemData();
        this.LoadItemSender();
        this.LoadDespawn();
        this.LoadModel();
    }

    protected virtual void LoadItemData()
    {
        if (this.itemData != null) return;
        string resPath = "Item/" + transform.name.Remove(0, 5);
        this.itemData = Resources.Load<ItemData>(resPath);
    }

    protected virtual void LoadItemSender()
    {
        this.itemSender = transform.GetComponentInChildren<ItemSender>();
    }

    protected virtual void LoadDespawn()
    {
        this.despawn = transform.GetComponentInChildren<Despawn>();
    }

    protected virtual void LoadModel()
    {
        this.itemModel = transform.Find("Model")?.GetComponent<SpriteRenderer>();
        if (this.itemModel == null)
        {
            Debug.Log("Item " + transform.name + " Model not found");
            return;
        }
        if (this.itemData == null) return;
        if (this.itemData.type == ITEM_TYPE.PLAYER_SKILL)
        {
            this.itemModel.sprite = this.itemData.skillData.image;
        }
    }
}
