using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReceiver : PMonoBehaviour
{
    public virtual void GetItemEffect(string effectName)
    {
        Transform newEffect = EffectSpawner.Instance.Spawn(effectName, transform.parent.position);
        newEffect.gameObject.SetActive(true);
    }
}
