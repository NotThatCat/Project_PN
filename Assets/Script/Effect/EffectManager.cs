using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : PMonoBehaviour
{
    private static EffectManager instance;
    public static EffectManager Instance => instance;
    [SerializeField] public string holderName = "EffectHolder";
    [SerializeField] public Transform effectHolder;
    [SerializeField] public string effectListName = "EffectList";
    [SerializeField] public List<Transform> effects;

    protected override void Start()
    {
        LoadComponents();
        this.HideAll();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEffectHolder();
        this.LoadEffects();
        this.HideAll();
    }

    protected override void Awake()
    {
        if (EffectManager.instance != this && EffectManager.instance != null) Debug.Log("Only allow one EffectManager");
        EffectManager.instance = this;
    }

    protected virtual void LoadEffectHolder()
    {
        this.effectHolder = GameObject.Find(this.holderName).transform;
    }

    protected virtual void LoadEffects()
    {
        Transform effectList = GameObject.Find(this.effectListName).transform;
        if (effectList == null)
        {
            Debug.Log(this.effectListName + " not found");
        }
        foreach (Transform effect in effectList)
        {
            this.effects.Add(effect);
        }
    }

    protected virtual void HideAll()
    {
        foreach (Transform effect in this.effects)
        {
            effect.gameObject.SetActive(false);
        }
    }

    public virtual Transform Spawn(string effectName, Vector3 spawnPosition)
    {
        Transform effectPrefab = this.GetEffectByName(effectName);
        Transform newEffect = Instantiate(effectPrefab);
        newEffect.position = spawnPosition;
        newEffect.parent = this.effectHolder;
        newEffect.gameObject.SetActive(true);
        return newEffect;
    }

    public virtual Transform Spawn(string effectName, Vector3 spawnPosition, Quaternion rotation)
    {
        Transform effectPrefab = this.GetEffectByName(effectName);
        Transform newEffect = Instantiate(effectPrefab, spawnPosition, rotation, this.effectHolder);
        newEffect.gameObject.SetActive(true);
        return newEffect;
    }

    public virtual Transform GetEffectByName(string effectName)
    {
        foreach (Transform effect in this.effects)
        {
            if (effect.name == effectName) return effect;
        }
        return null;
    }

}
