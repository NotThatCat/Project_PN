using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : PMonoBehaviour
{
    [SerializeField] protected string waveHolderName = "WaveList";
    [SerializeField] protected Transform waveHolder;
    [SerializeField] protected List<WaveData> waveDatas;
    [SerializeField] protected List<Transform> waveList;
    [SerializeField] protected List<StateWave> currentStateWave;
    [SerializeField] public int currentWave = 0;
    [SerializeField] protected bool isRunningWave = false;
    [SerializeField] protected bool loopState = true;
    [SerializeField] protected float processDelay = 0.01f;
    [SerializeField] protected float waveTime = 0f;
    [SerializeField] protected float nextWaveTime = 0f;

    public static WaveManager instance;

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

    public virtual void LoadStateWave(List<StateWave> states)
    {
        this.ResetWave();

        foreach (StateWave state in states)
        {
            this.SpawnWave(state.waveId);
            this.currentStateWave.Add(state);
        }
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

    protected virtual void SpawnWave(WAVE_ID waveId)
    {
        if (this.waveHolder.Find(waveId.ToString()) != null) return;

        Transform newWave = new GameObject(waveId.ToString(), typeof(WaveCtrl)).transform;
        newWave.transform.parent = this.waveHolder;
        newWave.gameObject.SetActive(false);

        WaveCtrl wctrl = newWave.GetComponent<WaveCtrl>();
        wctrl.CreateWave(waveId);

        this.waveList.Add(newWave);
    }

    public virtual void DeSpawnWave(WAVE_ID waveId)
    {
        Transform wave = this.waveHolder.Find(waveId.ToString());
        if (wave == null) return;

        WaveCtrl wctrl = wave.GetComponent<WaveCtrl>();
        wctrl.ResetWave();
        wave.gameObject.SetActive(false);
    }

    protected virtual IEnumerator ProcessingWave()
    {

        this.waveTime = 0;
        this.nextWaveTime = this.waveTime;

        for (int i = 0; i < this.currentStateWave.Count; i++)
        {
            while (this.currentStateWave[i].spawnAt > this.waveTime)
            {
                this.waveTime += this.processDelay;
                yield return new WaitForSeconds(this.processDelay);
            }

            this.StartSpawning(this.currentStateWave[i].waveId.ToString());
        }

        if (this.loopState)
        {
            this.ReRunState();
        }

        this.waveTime = 0;
    }

    protected virtual void ResetWave()
    {
        foreach (Transform wave in this.waveList)
        {
            Destroy(wave.gameObject);
        }

        this.waveList = new List<Transform>();
        this.currentStateWave = new List<StateWave>();
    }

    protected virtual void ReRunState()
    {
        this.ResetWaveStatus();
        this.StartLoadedState();
    }

    protected virtual void ResetWaveStatus()
    {
        foreach (Transform waveObj in this.waveList)
        {
            waveObj.gameObject.SetActive(false);
            WaveCtrl wctrl = waveObj.GetComponent<WaveCtrl>();
            wctrl.ResetWave();
        }
    }

    protected override void Start()
    {

    }

    protected virtual void StartSpawning(string waveID)
    {
        Transform wave = this.waveHolder.Find(waveID);
        wave.gameObject.SetActive(true);
        WaveCtrl waveCtrl = wave.GetComponent<WaveCtrl>();
        waveCtrl.StartSpawning();

    }

}
