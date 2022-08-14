using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : PMonoBehaviour
{
    [SerializeField] protected float speed = 12f;

    protected override void Update()
    {
        transform.parent.Translate(Vector3.up * this.speed * Time.deltaTime);
    }
}
