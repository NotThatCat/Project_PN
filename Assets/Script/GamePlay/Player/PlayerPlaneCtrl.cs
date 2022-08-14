using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaneCtrl : PlaneCtrl
{
    [Header("Player")]
    [SerializeField] protected PlayerCtrl playerCtrl;

    protected override void LoadCtrl()
    {
        base.LoadCtrl();
        this.LoadPlayCtrl();
    }

    protected virtual void LoadPlayCtrl()
    {
        this.playerCtrl = transform.GetComponentInParent<PlayerCtrl>();
    }
}
