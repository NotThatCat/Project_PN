using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManagerTest : PMonoBehaviour
{
    [SerializeField] protected string waveHolderName = "WaveList";
    [SerializeField] protected Transform waveHolder;
    [SerializeField] protected List<WaveData> waveDatas;
    [SerializeField] protected List<Transform> waveList;
    [SerializeField] protected List<StateWave> currentStateWave;
    [SerializeField] protected List<WaveCtrlTest> waveCtrls;
    [SerializeField] public int currentWave = 0;
    [SerializeField] protected bool isRunningWave = false;
    [SerializeField] protected bool loopAllWave = true;
    [SerializeField] protected float processDelay = 0.01f;
    [SerializeField] protected float waveTime = 0f;
    [SerializeField] protected float nextWaveTime = 0f;
    [SerializeField] protected float waveCount = 0;

    public static WaveManagerTest instance;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWaveData();
        this.LoadWaveHolder();
    }

    protected override void Awake()
    {
        if (!instance || instance != this)
        {
            instance = this;
        }
    }

    protected virtual void LoadWaveData()
    {
        if (this.waveDatas.Count > 0)
        {
            this.waveDatas = new List<WaveData>();
        }

        WaveData[] waveDatas = Resources.FindObjectsOfTypeAll(typeof(WaveData)) as WaveData[];

        foreach (WaveData waveData in waveDatas)
        {
            this.waveDatas.Add(waveData);
        }
    }

    protected virtual void LoadWaveHolder()
    {
        this.waveHolder = transform.Find(this.waveHolderName);
    }

    public virtual void StartLoadedState()
    {
        StartCoroutine(ProcessingWave());
    }

    public virtual bool StartWave(WAVE_ID waveId)
    {


        WaveCtrlTest wctrl = this.GetWaveCtrl(waveId);
        if (wctrl != null)
        {
            wctrl.StartSpawning();
            return true;
        }

        return false;
    }

    public virtual void LoadStateWave(List<StateWave> states)
    {
        this.ResetWave();

        foreach (StateWave state in states)
        {
            this.CreateWave(state.waveId);
            this.currentStateWave.Add(state);
        }
    }

    protected virtual void CreateWave(WAVE_ID waveId)
    {
        Transform newWave = new GameObject(waveId.ToString(), typeof(WaveCtrlTest)).transform;
        newWave.transform.parent = this.waveHolder;

        WaveCtrlTest wctrl = newWave.GetComponent<WaveCtrlTest>();
        wctrl.CreateWave(waveId);

        this.waveCtrls.Add(wctrl);
        this.waveList.Add(newWave);
    }

    protected virtual IEnumerator ProcessingWave()
    {

        this.waveTime = 0;
        this.nextWaveTime = this.waveTime;
        this.waveCount = 0;

        for (int i = 0; i < this.waveCtrls.Count; i++)
        {
            while(this.currentStateWave[i].spawnAt > this.waveTime)
            {
                this.waveTime += processDelay;
                yield return new WaitForSeconds(processDelay);
            }

            this.waveCount = i;
            this.waveCtrls[i].StartSpawning();
        }

        this.waveTime = 0;
    }

    public virtual WaveData GetWaveData(string name)
    {
        foreach (WaveData wd in this.waveDatas)
        {
            if (wd.name == name) return wd;
        }

        return null;
    }

    public virtual WaveData GetWaveDataByID(WAVE_ID id)
    {
        foreach (WaveData wd in this.waveDatas)
        {
            if (wd.waveId == id) return wd;
        }

        return null;
    }

    protected virtual WaveCtrlTest GetWaveCtrl(WAVE_ID waveId)
    {
        foreach (StateWave sw in this.currentStateWave)
        {
            if (sw.waveId == waveId)
            {
                return this.waveCtrls[this.currentStateWave.IndexOf(sw)];
            }
        }
        return null;
    }

    protected virtual void ResetWave()
    {
        foreach (Transform wave in this.waveList)
        {
            Destroy(wave.gameObject);
        }

        this.waveList = new List<Transform>();
        this.waveCtrls = new List<WaveCtrlTest>();
        this.currentStateWave = new List<StateWave>();
    }

    protected virtual void ResetWaveStatus()
    {
        foreach (WaveCtrlTest wctrl in this.waveCtrls)
        {
            wctrl.ResetWave();
        }
    }

    protected override void Start()
    {

    }
}
