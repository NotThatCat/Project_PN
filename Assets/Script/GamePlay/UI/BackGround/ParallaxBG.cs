using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : PMonoBehaviour
{
    [SerializeField] protected float length;
    [SerializeField] protected float distance;
    [SerializeField] protected Vector3 basePos;
    [SerializeField] protected Vector3 virtualPos;
    [SerializeField] protected Vector3 virtualStartPos;
    [SerializeField] protected Vector3 virtualCamPos;
    [SerializeField] protected float parallaxEffectX;
    [SerializeField] protected float parallaxEffectY;

    [SerializeField] protected float movingSpeed = 3f;
    [SerializeField] protected Transform player;
    [SerializeField] protected float followX;
    [SerializeField] protected string playerName = "MyPlayer";

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayer();
    }

    public virtual void LoadPlayer()
    {
        this.followX = 0;
        this.player = GameObject.Find(this.playerName).transform;
        if (this.player) this.followX = this.player.position.x;
    }

    public virtual void UpdateParallaxBG(float newParallaxBG)
    {
        this.parallaxEffectX = newParallaxBG;
        this.parallaxEffectY = newParallaxBG;
    }

    public virtual void UpdateParallax(float newParallaxBGX, float newParallaxBGY)
    {
        this.parallaxEffectX = newParallaxBGX;
        this.parallaxEffectY = newParallaxBGY;
    }

    protected override void Start()
    {
        this.LoadPlayer();
        this.basePos = transform.position;
        this.virtualPos = transform.position;
        this.virtualStartPos = transform.position;
        this.virtualCamPos = this.transform.position;
        this.length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    protected override void Update()
    {
        this.virtualCamPos.y += Time.deltaTime * this.movingSpeed;

        if (this.player) this.followX = this.player.position.x;

        this.distance = (this.virtualCamPos.y * (1 - this.parallaxEffectX));
        Vector2 dist = new Vector2(this.followX * this.parallaxEffectX, this.virtualCamPos.y * this.parallaxEffectY);

        this.virtualPos = new Vector3(this.virtualStartPos.x + dist.x, this.virtualStartPos.y + dist.y, transform.position.z);
        transform.position = new Vector3(this.basePos.x, this.basePos.y + dist.y % this.length, transform.position.z);

        if (distance > this.virtualPos.y + length)
        {
            this.virtualStartPos.y += this.length;
        }
        else if (distance < this.virtualPos.y - length)
        {
            this.virtualStartPos.y -= this.length;
        }

        if (transform.position.y > this.length)
        {
            transform.position = new Vector3(this.basePos.x, this.basePos.y - length, this.basePos.z);
        }
        else if (transform.position.y < -this.length)
        {
            transform.position = new Vector3(this.basePos.x, this.basePos.y + length, this.basePos.z);
        }
    }

    protected virtual void ResetPosition()
    {
    }
}
