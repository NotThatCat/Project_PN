using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikePointCtrl : PMonoBehaviour
{
    [SerializeField] public List<Transform> strikePoints;

    protected override void Awake()
    {
        base.Awake();
        this.LoadComponents();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStrikePoint();
    }

    protected virtual void LoadStrikePoint()
    {
        foreach(Transform strikePoint in transform)
        {
            strikePoints.Add(strikePoint);
        }
    }

    public virtual List<Transform> GetStrikePoints()
    {
        return this.strikePoints;
    }
}
