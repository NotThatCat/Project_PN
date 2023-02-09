using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCam : PMonoBehaviour
{
    [SerializeField] protected Vector2 startpos;
    [SerializeField] protected Vector3 playerPosition;
    [SerializeField] protected float parallaxEffectX = 0.01f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    protected override void Start()
    {
        this.startpos = new Vector2(transform.position.x, transform.position.y);
    }

    protected override void Update()
    {
        this.GetPlayerPosition();

        Vector2 dist = new Vector2(this.playerPosition.x * parallaxEffectX, 0);

        transform.position = new Vector3(this.startpos.x + dist.x, 0, transform.position.z);
    }

    protected virtual void GetPlayerPosition()
    {
        this.playerPosition = GameManager.instance.GetPlayerPosition();
    }
}
