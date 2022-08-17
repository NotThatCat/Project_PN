using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : PMonoBehaviour
{
    [SerializeField] public static PlayerCtrl instance;
    [SerializeField] public PlayerMoving playerMovingCtrl;
    [SerializeField] public PlayerAttack playerAttack;
    [SerializeField] public PlayerPlaneCtrl playerModelCtrl;

    protected override void Awake()
    {
        if (PlayerCtrl.instance != null) Debug.LogError("Only 1 PlayerCtrl allow", gameObject);
        PlayerCtrl.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerModelCtrl();
        this.LoadPlayerMoving();
        this.LoadPlayerAttack();
    }

    protected virtual void LoadPlayerModelCtrl()
    {
        this.playerModelCtrl = transform.GetComponentInChildren<PlayerPlaneCtrl>();
    }

    protected virtual void LoadPlayerMoving()
    {
        this.playerMovingCtrl = transform.GetComponentInChildren<PlayerMoving>();
    }

    protected virtual void LoadPlayerAttack()
    {
        this.playerAttack = transform.GetComponentInChildren<PlayerAttack>();
    }

    public virtual void ChangeSkill(int idx)
    {
        this.playerAttack.ChangeSkill(idx);
    }

    public virtual void NextSkill(int idx)
    {
        this.playerAttack.NextSkill(idx);
    }

    public virtual void Attack()
    {
        this.playerAttack.Attack();
    }
}
