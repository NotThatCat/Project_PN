using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCtrl : PMonoBehaviour
{
    [Header("Model")]
    [SerializeField] public PlaneAnimator planeAnimator;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }

    protected virtual void LoadAnimator()
    {
        this.planeAnimator = transform.GetComponentInChildren<PlaneAnimator>();
    }

}
