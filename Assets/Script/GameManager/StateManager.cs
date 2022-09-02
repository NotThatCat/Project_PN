using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateManager : PMonoBehaviour
{
    public static StateManager instance;
    [SerializeField] public int currentStateIndex = 0;
    [SerializeField] public STATE_ID currentStateID;
    [SerializeField] public List<StateWave> currentStateWave;
    [SerializeField] public List<StateSO> stateList;

    protected override void Awake()
    {
        if (!instance || instance != this)
        {
            instance = this;
        }
    }

    protected override void LoadComponents()
    {
        this.LoadLevelSO();
    }

    protected virtual void LoadLevelSO()
    {
        if (this.stateList.Count > 0)
        {
            this.stateList = new List<StateSO>();
        }

        StateSO[] stateDatas = Resources.FindObjectsOfTypeAll(typeof(StateSO)) as StateSO[];

        foreach (StateSO state in stateDatas)
        {
            this.stateList.Add(state);
        }
    }

    public virtual StateSO GetCurrentStateSO()
    {
        return stateList[this.currentStateIndex];
    }

    public virtual STATE_ID GetCurrentStateID()
    {
        return this.currentStateID;
    }

    public virtual int NextState()
    {
        if (this.currentStateIndex + 1 >= stateList.Count)
        {
            return this.currentStateIndex;
        }
        else
        {
            return this.currentStateIndex++;
        }
    }

    public virtual void StartState(int state)
    {
        this.GoToState(state);
        this.StartLoadedState();
    }

    public virtual void StartState(STATE_ID stateId)
    {
        this.GoToState(stateId);
        this.StartLoadedState();
    }

    public virtual void StartLoadedState()
    {
        WaveManagerTest.instance.StartLoadedState();
        //StartCoroutine(ProcessingWave());
    }

    protected virtual int GoToState(int state)
    {
        if (this.ValidateState(state))
        {
            this.LoadStateWave(state);
            return this.currentStateIndex;
        }
        else
        {
            Debug.Log("GoToState fail");
            return this.currentStateIndex;
        }
    }

    protected virtual STATE_ID GoToState(STATE_ID stateId)
    {
        if (this.ValidateState(stateId))
        {
            this.LoadStateWave(stateId);
            return this.currentStateID;
        }
        else
        {
            Debug.Log("GoToState fail");
            return this.currentStateID;
        }
    }

    protected virtual bool ValidateState(int state)
    {
        if (state >= this.stateList.Count || state < 0)
        {
            Debug.Log("State invalid");
            return false;
        }
        else
        {
            return true;
        }
    }

    protected virtual bool ValidateState(STATE_ID stateId)
    {
        foreach (StateSO stateSo in this.stateList)
        {
            if (stateSo.stateId == stateId)
            {
                return true;
            }
        }
        return false;
    }

    protected virtual void LoadStateWave()
    {
        this.currentStateWave = new List<StateWave>();
        foreach (StateWave sw in this.stateList[this.currentStateIndex].stateWaves)
        {
            this.currentStateWave.Add(sw);
        }
        this.SortWave();
        WaveManagerTest.instance.LoadStateWave(this.currentStateWave);
    }

    protected virtual void LoadStateWave(int state)
    {
        this.currentStateIndex = state;
        this.currentStateID = this.stateList[state].stateId;
        this.LoadStateWave();
    }

    protected virtual void LoadStateWave(STATE_ID stateId)
    {
        for (int idx = 0; idx < this.stateList.Count; idx++)
        {
            if (this.stateList[idx].stateId == stateId)
            {
                this.currentStateIndex = idx;
                this.currentStateID = stateId;
                this.LoadStateWave();
                return;
            }
        }
        Debug.Log("LoadStateWave by STATE_ID Fail, cannot find STATE_ID");
    }

    protected virtual void SortWave()
    {
        this.currentStateWave.OrderBy(spawnTime => spawnTime.spawnAt);
        //this.currentStateWave.Sort();
    }
}
