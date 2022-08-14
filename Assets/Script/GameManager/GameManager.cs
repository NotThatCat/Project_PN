using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PMonoBehaviour
{
    public static GameManager instance;
    [SerializeField] protected string playerName = "MyPlayer";
    [SerializeField] protected Transform player;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayer();
    }

    protected virtual void LoadPlayer()
    {
        this.player = GameObject.Find(playerName).transform;
    }

    protected override void Start()
    {
        if (!instance || instance != this)
        {
            instance = this;
        }

        WaveManager.instance.StartWave();
    }

    public virtual Vector3 GetPlayerPosition()
    {
        return player.position;
    }
}
