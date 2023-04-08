using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSender : ItemSender
{
    [SerializeField] string skillName;

    protected override void ItemInteraction()
    {
        PlayerCtrl.instance.ChangeSkill(skillName);
        this.itemCtrl.despawn.Despawning();
    }

    protected override void LoadComponents()
    {
        this.LoadSkillName();
        base.LoadComponents();
    }

    private void LoadSkillName()
    {
        this.skillName = transform.parent.name.Remove(0, 5);
    }
}
