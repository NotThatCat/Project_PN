using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected int currentSkill = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayCtrl();
    }

    protected virtual void LoadPlayCtrl()
    {
        this.playerCtrl = transform.GetComponentInParent<PlayerCtrl>();
    }

    public virtual void Attack(int idx)
    {
        playerCtrl.playerModelCtrl.attackCtrl.Attack(idx);
    }

    public virtual void Attack()
    {
        playerCtrl.playerModelCtrl.attackCtrl.Attack();
    }

    public virtual void ChangeSkill(int idx)
    {
        this.currentSkill = playerCtrl.playerModelCtrl.attackCtrl.ChangeSkill(idx);
    }

    public virtual void NextSkill(int idx)
    {
        if (idx != 1 && idx != -1) return;
        this.ChangeSkill(this.currentSkill + idx);
    }
}
