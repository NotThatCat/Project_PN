using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : PMonoBehaviour
{
    [SerializeField] protected string waveListName = "WaveList";
    [SerializeField] protected List<Transform> waveList;
    [SerializeField] protected List<WaveData> waveDatas;
    [SerializeField] protected List<WaveCtrl> waveCtrls;
    [SerializeField] public int currentWave = 0;
    [SerializeField] protected bool isRunningWave = false;

    public static WaveManager instance;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWaveData();
        this.LoadWave();
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

    protected virtual void LoadWave()
    {
        this.waveList = new List<Transform>();
        this.waveCtrls = new List<WaveCtrl>();
        Transform waves = GameObject.Find(this.waveListName).transform;
        foreach (Transform w in waves)
        {
            this.waveList.Add(w);
            this.waveCtrls.Add(w.GetComponent<WaveCtrl>());
        }
    }

    public void StartWave()
    {
        if (!isRunningWave)
        {
            isRunningWave = true;
            StartCoroutine(ProcessingWave());
        }
    }

    protected virtual IEnumerator ProcessingWave()
    {
        isRunningWave = true;
        foreach (WaveCtrl wctrl in this.waveCtrls)
        {
            yield return new WaitForSeconds(wctrl.waveData.delayBeforeSpawn);
            wctrl.StartSpawning();
            while (!wctrl.IsWaveComplete())
            {
                yield return new WaitForSeconds(wctrl.waveData.spawnBetweenDelay);
            }
            yield return new WaitForSeconds(wctrl.waveData.delayAfterSpawn);
        }
        isRunningWave = false;

        yield return new WaitForSeconds(0.5f);
        this.ResetWave();
        this.StartWave();
    }

    public virtual WaveData GetWaveData(string name)
    {
        foreach (WaveData wd in this.waveDatas)
        {
            if (wd.name == name) return wd;
        }

        return null;
    }

    protected virtual void ResetWave()
    {
        foreach (WaveCtrl wctrl in this.waveCtrls)
        {
            wctrl.ResetWave();
        }
    }
}
