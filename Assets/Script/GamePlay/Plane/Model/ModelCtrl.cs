using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCtrl : PMonoBehaviour
{
    [SerializeField] protected  Animator animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }

    protected virtual void LoadAnimator()
    {
        this.animator = transform.GetComponent<Animator>();
    }

    public Animator GetAnimator()
    {
        return this.animator;
    }
}
