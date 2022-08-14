using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAnimator : PMonoBehaviour
{
    [SerializeField] protected PLANE_ANIMATION_STATE state;
    [SerializeField] protected Animator animator;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }

    protected virtual void LoadAnimator()
    {
        this.animator = transform.GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        switch (this.state)
        {
            case PLANE_ANIMATION_STATE.Idle:
                this.UpdateAnimator("Left", false);
                this.UpdateAnimator("Right", false);
                break;
            case PLANE_ANIMATION_STATE.TurnLeft:
                this.UpdateAnimator("Left", true);
                this.UpdateAnimator("Right", false);
                break;
            case PLANE_ANIMATION_STATE.TurnRight:
                this.UpdateAnimator("Left", false);
                this.UpdateAnimator("Right", true);
                break;
        }
    }

    public virtual void TurnLeft()
    {
        this.state = PLANE_ANIMATION_STATE.TurnLeft;
    }

    public virtual void TurnRight()
    {
        this.state = PLANE_ANIMATION_STATE.TurnRight;
    }

    public virtual void Idle()
    {
        this.state = PLANE_ANIMATION_STATE.Idle;
    }

    protected virtual void UpdateAnimator(string key, bool value)
    {
        animator.SetBool(key, value);
    }
}
