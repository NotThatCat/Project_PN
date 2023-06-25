//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WaveManager : PMonoBehaviour
//{
//    [SerializeField] protected string waveHolderName = "WaveList";
//    [SerializeField] protected Transform waveHolder;
//    [SerializeField] protected List<WaveData> waveDatas;
//    [SerializeField] protected List<Transform> waveList;
//    [SerializeField] protected List<StateWave> currentStateWave;
//    [SerializeField] protected List<WaveCtrl> waveCtrls;
//    [SerializeField] public int currentWave = 0;
//    [SerializeField] protected bool isRunningWave = false;
//    [SerializeField] protected bool loopState = true;
//    [SerializeField] protected float processDelay = 0.01f;
//    [SerializeField] protected float waveTime = 0f;
//    [SerializeField] protected float nextWaveTime = 0f;

//    public static WaveManager instance;

//    protected override void LoadComponents()
//    {
//        base.LoadComponents();
//        this.LoadWaveData();
//        this.LoadWaveHolder();
//    }

//    protected override void Awake()
//    {
//        if (!instance || instance != this)
//        {
//            instance = this;
//        }
//    }

//    protected virtual void LoadWaveData()
//    {
//        if (this.waveDatas.Count > 0)
//        {
//            this.waveDatas = new List<WaveData>();
//        }

//        WaveData[] waveDatas = Resources.FindObjectsOfTypeAll(typeof(WaveData)) as WaveData[];

//        foreach (WaveData waveData in waveDatas)
//        {
//            this.waveDatas.Add(waveData);
//        }
//    }

//    protected virtual void LoadWaveHolder()
//    {
//        this.waveHolder = transform.Find(this.waveHolderName);
//    }

//    public virtual void StartLoadedState()
//    {
//        StartCoroutine(ProcessingWave());
//    }

//    public virtual void LoadStateWave(List<StateWave> states)
//    {
//        this.ResetWave();

//        foreach (StateWave state in states)
//        {
//            this.CreateWave(state.waveId);
//            this.currentStateWave.Add(state);
//        }
//    }

//    public virtual WaveData GetWaveData(string name)
//    {
//        foreach (WaveData wd in this.waveDatas)
//        {
//            if (wd.name == name) return wd;
//        }

//        return null;
//    }

//    public virtual WaveData GetWaveDataByID(WAVE_ID id)
//    {
//        foreach (WaveData wd in this.waveDatas)
//        {
//            if (wd.waveId == id) return wd;
//        }

//        return null;
//    }

//    protected virtual void CreateWave(WAVE_ID waveId)
//    {
//        Transform newWave = new GameObject(waveId.ToString(), typeof(WaveCtrl)).transform;
//        newWave.transform.parent = this.waveHolder;

//        WaveCtrl wctrl = newWave.GetComponent<WaveCtrl>();
//        wctrl.CreateWave(waveId);

//        this.waveCtrls.Add(wctrl);
//        this.waveList.Add(newWave);
//    }

//    protected virtual IEnumerator ProcessingWave()
//    {

//        this.waveTime = 0;
//        this.nextWaveTime = this.waveTime;

//        for (int i = 0; i < this.waveCtrls.Count; i++)
//        {
//            while(this.currentStateWave[i].spawnAt > this.waveTime)
//            {
//                this.waveTime += processDelay;
//                yield return new WaitForSeconds(processDelay);
//            }

//            this.waveCtrls[i].StartSpawning();
//        }

//        if (this.loopState)
//        {
//            this.ReRunState();
//        }

//        this.waveTime = 0;
//    }

//    protected virtual WaveCtrl GetWaveCtrl(WAVE_ID waveId)
//    {
//        foreach (StateWave sw in this.currentStateWave)
//        {
//            if (sw.waveId == waveId)
//            {
//                return this.waveCtrls[this.currentStateWave.IndexOf(sw)];
//            }
//        }
//        return null;
//    }

//    protected virtual void ResetWave()
//    {
//        foreach (Transform wave in this.waveList)
//        {
//            Destroy(wave.gameObject);
//        }

//        this.waveList = new List<Transform>();
//        this.waveCtrls = new List<WaveCtrl>();
//        this.currentStateWave = new List<StateWave>();
//    }

//    protected virtual void ReRunState()
//    {
//        this.ResetWaveStatus();
//        this.StartLoadedState();
//    }

//    protected virtual void ResetWaveStatus()
//    {
//        foreach (WaveCtrl wctrl in this.waveCtrls)
//        {
//            wctrl.ResetWave();
//        }
//    }

//    protected override void Start()
//    {

//    }
//}
