using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplosionEffect : PMonoBehaviour
{
    [SerializeField] protected List<Transform> explorePos;
    [SerializeField] protected string listPosName = "Pos";
    [SerializeField] protected string effectName = "ExploreWhite";
    [SerializeField] protected bool loop = true;
    [SerializeField] protected float delay = 0.1f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadExplorePos();
    }

    protected virtual void LoadExplorePos()
    {
        this.explorePos = new List<Transform>();

        Transform listPos = transform.Find(this.listPosName);
        foreach(Transform pos in listPos)
        {
            this.explorePos.Add(pos);
        }
    }

    protected override void Start()
    {
        StartCoroutine(ExploreEffect());
    }

    protected virtual IEnumerator ExploreEffect()
    {
        while (this.loop)
        {
            foreach (Transform pos in this.explorePos)
            {
                EffectManager.instance.Spawn(this.effectName, pos.position);
                yield return new WaitForSeconds(this.delay);
            }
        }
    }
}
