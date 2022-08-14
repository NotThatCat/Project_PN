using UnityEngine;

public class PlayerMoving : PMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected float animationScale = 0.15f;
    [SerializeField] protected float speed = 7f;
    [SerializeField] protected Vector2 position;
    [SerializeField] protected Vector2 limitHorizon = new Vector2(-3.25f, 3.25f);
    [SerializeField] protected Vector2 limitVertical = new Vector2(4.5f, -4.5f);

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        this.playerCtrl = transform.parent.GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        this.GetPositionByMouse();
        this.Move2Position();
        this.UpdateAnimation();
    }

    protected virtual void Move2Position()
    {
        Vector3 targetPos = new Vector3(this.position.x, this.position.y, transform.parent.position.z);
        Vector3 newPositionPerFrame = Vector3.MoveTowards(transform.parent.position, targetPos, this.speed * Time.deltaTime);
        transform.parent.position = newPositionPerFrame;
    }

    protected virtual void GetPositionByMouse()
    {
        this.position = InputsManager.instance.GetMousePos();
        if (this.position.x < this.limitHorizon.x) this.position.x = this.limitHorizon.x;
        if (this.position.x > this.limitHorizon.y) this.position.x = this.limitHorizon.y;

        if (this.position.y > this.limitVertical.x) this.position.y = this.limitVertical.x;
        if (this.position.y < this.limitVertical.y) this.position.y = this.limitVertical.y;
    }

    protected virtual void UpdateAnimation()
    {
        /// Check moving direction
        if (this.position.x - transform.position.x >= 0 + this.animationScale)
        {
            this.playerCtrl.playerModelCtrl.planeAnimator.TurnRight();
        }
        else if (this.position.x - transform.position.x <= 0 - this.animationScale)
        {
            this.playerCtrl.playerModelCtrl.planeAnimator.TurnLeft();
        }
        else
        {
            this.playerCtrl.playerModelCtrl.planeAnimator.Idle();
        }
    }
}
