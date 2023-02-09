using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReceiver : PMonoBehaviour
{
    public virtual void GetItemEffect(string effectName)
    {
        EffectManager.instance.Spawn(effectName, transform.parent.position);
    }
}
