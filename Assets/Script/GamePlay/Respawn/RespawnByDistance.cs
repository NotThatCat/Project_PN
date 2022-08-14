using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnByDistance : Respawn
{
    public float respawnLimit = 18f;
    public float distance = 0;
    public Transform mainCam;
    public string camName = "Main Camera";

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
        this.distance = Vector2.Distance(transform.position, this.mainCam.position);
        if (this.distance > this.respawnLimit) this.Respawning();
    }

    protected virtual void LoadCam()
    {
        this.mainCam = GameObject.Find(this.camName).transform;
    }
}
