using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : Despawn
{
    [SerializeField] protected float despawnLimitX = 10f;
    [SerializeField] protected float despawnLimitY = 3.5f;
    [SerializeField] protected float distanceX = 0;
    [SerializeField] protected float distanceY = 0;
    [SerializeField] protected Transform mainCam;
    [SerializeField] protected string camName = "Main Camera";

    protected override void OnEnable()
    {
        base.OnEnable();
        this.LoadComponents();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCam();
    }

    protected override void Start()
    {
        this.LoadComponents();
    }

    protected override void FixedUpdate()
    {
        this.DespawnByDistancing();
    }

    protected virtual void DespawnByDistancing()
    {
        //this.distance = Vector2.Distance(transform.position, this.mainCam.position);
        this.distanceX = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(this.mainCam.position.x, 0));
        this.distanceY = Vector2.Distance(new Vector2(0, transform.position.y), new Vector2(0, this.mainCam.position.y));
        if (this.distanceX > this.despawnLimitX) this.Despawning();
        if (this.distanceY > this.despawnLimitY) this.Despawning();
    }

    protected virtual void LoadCam()
    {
        this.mainCam = GameObject.Find(this.camName).transform;
    }
}
