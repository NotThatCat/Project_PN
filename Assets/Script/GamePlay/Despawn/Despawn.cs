using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : PMonoBehaviour
{
    /// <summary>
    /// Starting point of the despawning process
    /// </summary>
    public virtual void Despawning()
    {
        if (this.ReadyToDespawn())
        {
            this.DespawnNow();
            this.AfterDespawan();
        }
    }

    /// <summary>
    /// When you call Despawning, are you sure this is the time to despawn? This will prevent any unwanted despawn.
    /// </summary>
    /// <returns>bool, Default true</returns>
    protected virtual bool ReadyToDespawn()
    {
        return true;
    }

    /// <summary>
    /// Despawn function, Example: Some object will need to destroy transform.parent.parent
    /// </summary>
    protected virtual void DespawnNow()
    {
        Destroy(transform.parent.gameObject);
    }

    /// <summary>
    /// Need to Instantiate any effect after despawn?
    /// </summary>
    protected virtual void AfterDespawan()
    {
        /// For any effect after destroy object
    }

}
