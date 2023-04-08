using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PMonoBehaviour
{
    public static GameManager instance;
    [SerializeField] protected string playerName = "MyPlayer";
    [SerializeField] protected Transform player;
    [SerializeField] protected Vector3 lastPlayerPostion;
    [SerializeField] protected bool toogleState = false;
    [SerializeField] public STATE_ID currentStateID;

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

        this.StartGame();
    }

    internal void PlayerDeath()
    {
        InputsManager.instance.DisableInGameInput();
    }

    public virtual Vector3 GetPlayerPosition()
    {
        if (this.player != null)
        {
            this.lastPlayerPostion = this.player.position;
        }

        return this.lastPlayerPostion;
    }

    public void StartGame()
    {
        if (toogleState)
        {
            StateManager.instance.StartState(this.currentStateID);
        }
        this.EnablePlayer();
    }

    public virtual void EnablePlayer()
    {
        PlayerCtrl.instance.playerMovingCtrl.EnableMoving();
    }

    public virtual void PauseGame()
    {
        Time.timeScale = 0;
        SoundManager.instance.PauseMusic();
        UIManager.instance.ShowUIPauseGame();
        PlayerCtrl.instance.playerMovingCtrl.DisableMoving();
    }

    public virtual void ResumeGame()
    {
        Time.timeScale = 1;
        SoundManager.instance.ResumeMusic();
        UIManager.instance.ShowUIIngame();
        PlayerCtrl.instance.playerMovingCtrl.EnableMoving();
    }

    public virtual void PlayerLevelUp()
    {
        PlayerCtrl.instance.level.Up();
    }

    public virtual void PlayerLevelDown()
    {
        PlayerCtrl.instance.level.Down();
    }
}
