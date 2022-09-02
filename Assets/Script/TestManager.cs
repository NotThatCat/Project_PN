using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : PMonoBehaviour
{
    [SerializeField] public bool test = false;
    [SerializeField] public StateSO state;
    [SerializeField] public STATE_ID stateId;

    public static TestManager instance;

    protected override void Awake()
    {
        if (!instance || instance != this)
        {
            instance = this;
        }
    }

    protected override void Start()
    {
        if (this.test)
        {
            this.state = StateManager.instance.GetCurrentStateSO();
            this.stateId = StateManager.instance.GetCurrentStateID();
            //WaveManagerTest.instance.LoadStateWave(this.state.stateWaves);
            StateManager.instance.StartState(this.stateId);
        }
    }

}
