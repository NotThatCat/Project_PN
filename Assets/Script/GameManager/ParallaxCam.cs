using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCam : PMonoBehaviour
{
    [SerializeField] protected Vector2 startpos;
    [SerializeField] protected string playerName = "MyPlayer";
    [SerializeField] protected Transform player;
    [SerializeField] protected float parallaxEffectX = 0.01f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayer();
    }

    public virtual void LoadPlayer()
    {
        this.player = GameObject.Find(this.playerName).transform;
    }

    protected override void Start()
    {
        this.startpos = new Vector2(transform.position.x, transform.position.y);
    }

    protected override void Update()
    {
        float temp = (this.player.position.x * (1 - parallaxEffectX));
        Vector2 dist = new Vector2(this.player.position.x * parallaxEffectX, 0);

        transform.position = new Vector3(this.startpos.x + dist.x, 0, transform.position.z);

        //if (temp > startpos.x + length)
        //{
        //    startpos.x += length;
        //}
        //else if (temp < startpos.x - length)
        //{
        //    startpos.x -= length;
        //}
    }
}
