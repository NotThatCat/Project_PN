using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : PMonoBehaviour
{
    public static ItemManager instance;
    [SerializeField] public string holderName = "ItemHolder";
    [SerializeField] public Transform itemHolder;
    [SerializeField] public List<Transform> items;

    protected override void Start()
    {
        LoadComponents();
        this.HideAll();
    }

    protected override void Awake()
    {
        if (ItemManager.instance != this && BulletManager.instance != null) Debug.Log("Only allow one ItemManager");
        ItemManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemHolder();
        this.LoadItems();
    }

    private void LoadItems()
    {
        foreach (Transform item in transform)
        {
            this.items.Add(item);
        }
    }

    private void LoadItemHolder()
    {
        this.itemHolder = GameObject.Find(this.holderName).transform;
    }

    public virtual Transform Spawn(string itemName, Vector3 spawnPosition)
    {
        Transform itemPrefab = this.GetItemByName(itemName);
        Transform newItem = Instantiate(itemPrefab);
        newItem.position = spawnPosition;
        newItem.parent = this.itemHolder;
        newItem.gameObject.SetActive(true);
        return newItem;
    }

    public virtual Transform Spawn(string itemName, Vector3 spawnPosition, Quaternion rotation)
    {
        Transform itemPrefab = this.GetItemByName(itemName);
        Transform newItem = Instantiate(itemPrefab, spawnPosition, rotation, this.itemHolder);
        newItem.gameObject.SetActive(true);
        return newItem;
    }

    protected virtual void HideAll()
    {
        foreach (Transform item in this.items)
        {
            item.gameObject.SetActive(false);
        }
    }

    public virtual Transform GetItemByName(string itemName)
    {
        foreach (Transform item in this.items)
        {
            if (item.name == itemName) return item;
        }
        return null;
    }

}
