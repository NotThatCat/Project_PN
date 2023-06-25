using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpin : PMonoBehaviour
{
    [SerializeField] public float speed = 5f;
    [SerializeField] protected float rotateSpeed = 1500f;
    [SerializeField] public Transform model;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRotateModel();
    }

    protected virtual void LoadRotateModel()
    {
        this.model = transform.parent;
    }

    protected override void Update()
    {
        this.rotate();
    }

    protected virtual void rotate()
    {
        this.model.Rotate(new Vector3(0, 0, 1), rotateSpeed * Time.deltaTime, Space.Self);
    }
}
