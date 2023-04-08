using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Spawner
{
    private static ItemSpawner instance;
    public static ItemSpawner Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (ItemSpawner.instance != null) Debug.LogError("Only 1 ItemSpawner allow to exist");
        ItemSpawner.instance = this;
    }

    public virtual void Drop(List<DropRate> dropList, Vector3 position, Quaternion rotation)
    {
        List<DropRate> possibleItems = GetPossibleList(dropList);

        if (possibleItems == null) return;
        foreach(DropRate drop in possibleItems)
        {
            Transform newDrop = this.Spawn(drop.itemName, position, rotation);
            newDrop.gameObject.SetActive(true);
        }
    }

    protected virtual List<DropRate> GetPossibleList(List<DropRate> dropList)
    {
        List<DropRate> possibleItems = new List<DropRate>();

        foreach (DropRate drop in dropList)
        {
            int randomNumber = Random.Range(1, 100000);
            if (randomNumber <= drop.dropRate)
            {
                possibleItems.Add(drop);
            }
        }

        if (possibleItems.Count > 0)
        {
            return possibleItems;
        }

        return null;
    }
}
