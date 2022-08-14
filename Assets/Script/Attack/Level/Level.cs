using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : PMonoBehaviour
{
    [SerializeField] public int level = 1;
    [SerializeField] public int maxLevel = 5;

    public virtual void Up(int value = 1)
    {
        if (this.level == this.maxLevel) return;
        this.level += value;
    }

    public virtual void Down(int value = 1)
    {
        if (this.level == 0) return;
        this.level -= value;
    }
}
