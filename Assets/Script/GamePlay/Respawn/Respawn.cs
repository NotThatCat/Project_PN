using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : PMonoBehaviour
{
    [SerializeField] protected Vector3 respawnPosition;

    /// <summary>
    /// Starting point of the despawning process
    /// </summary>
    public virtual void Respawning()
    {
        if (this.ReadyToRespawn())
        {
            this.RespawnNow();
            this.AfterRespawn();
        }
    }

    /// <summary>
    /// When you call Despawning, are you sure this is the time to despawn? This will prevent any unwanted despawn.
    /// </summary>
    /// <returns>bool, Default true</returns>
    protected virtual bool ReadyToRespawn()
    {
        return true;
    }

    /// <summary>
    /// Despawn function, Example: Some object will need to destroy transform.parent.parent
    /// </summary>
    protected virtual void RespawnNow()
    {
        this.transform.position = this.respawnPosition;
    }

    /// <summary>
    /// Need to Instantiate any effect after despawn?
    /// </summary>
    protected virtual void AfterRespawn()
    {
        /// For any effect after destroy object
    }

}
