using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : Level
{
    public override void Up(int value = 1)
    {
        if (this.level == this.maxLevel) return;
        this.level += value;
        this.UpdatePlayLevelBar();
    }

    public override void Down(int value = 1)
    {
        if (this.level == 0) return;
        this.level -= value;
        this.UpdatePlayLevelBar();
    }

    protected virtual void UpdatePlayLevelBar()
    {
        UIManager.instance.UpdatePlayerLevel((float)this.level / (float)this.maxLevel);
    }

    protected override void Start()
    {
        base.Start();
        this.UpdatePlayLevelBar();
    }
}
