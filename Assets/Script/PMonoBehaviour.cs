using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMonoBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void Awake()
    {
        this.LoadComponents();
    }

    protected virtual void Start()
    {
        //For override
    }

    protected virtual void Update()
    {
        //For override
    }

    protected virtual void FixedUpdate()
    {
        //For override
    }

    protected virtual void OnEnable()
    {
        this.ResetValue();
        //For override
    }

    protected virtual void OnDisable()
    {
        //For override
    }

    public virtual void ResetValue()
    {

    }

    protected virtual void LoadComponents()
    {
        /// For Override
    }

    protected virtual void LogError(string error)
    {
        Debug.LogError(transform.name + " " + error);
    }
}
