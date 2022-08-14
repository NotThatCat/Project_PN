using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneStrikePointCtrl : StrikePointCtrl
{
    [SerializeField] protected PlaneCtrl planeCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlaneCtrl();
    }

    protected virtual void LoadPlaneCtrl()
    {
        this.planeCtrl = transform.parent.GetComponentInChildren<PlaneCtrl>();
    }
}
