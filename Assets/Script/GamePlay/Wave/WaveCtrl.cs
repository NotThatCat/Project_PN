using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCtrl : PMonoBehaviour
{
    [SerializeField] protected WaveManager waveManager;
    [SerializeField] public WaveData waveData;
    [SerializeField] protected Transform path;
    [SerializeField] protected WAVE_STATUS waveStatus = WAVE_STATUS.REARY_TO_SPAWN;
    [SerializeField] protected int currentIndex;

    public virtual void ResetWave()
    {
        StopAllCoroutines();
        this.currentIndex = 0;
        this.waveStatus = WAVE_STATUS.REARY_TO_SPAWN;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWaveManager();
        this.LoadWaveInfo();
        this.LoadPath();
    }

    protected virtual void LoadWaveManager()
    {
        this.waveManager = transform.parent.parent.GetComponent<WaveManager>();
    }

    protected virtual void LoadWaveInfo()
    {
        if (this.waveData || this.waveData == null)
        {
            this.waveData = this.waveManager.GetWaveData(this.name);
        }
    }

    protected virtual void LoadPath()
    {
        //this.path = EnemyManager.instance.GetMovingPath(this.waveData.pathName);
        this.path = GameObject.Find(this.waveData.pathName).transform;
    }

    protected virtual IEnumerator SpawnEnemy()
    {
        this.waveStatus = WAVE_STATUS.SPAWNING;
        for (int i = this.currentIndex; i < this.waveData.enemyList.Count; i++)
        {
            Vector3 spawnPosition = this.path.GetChild(0).position;
            Transform newEnemy = EnemyManager.instance.SpawnEnemy(this.waveData.enemyList[i], spawnPosition);
            if (newEnemy != null)
            {
                MoveByPath newEnemyMoving = newEnemy.GetComponentInChildren<MoveByPath>();
                newEnemyMoving.LoadCheckPoints(this.path);
                newEnemyMoving.StartMoving();
            }
            else
            {
                Debug.Log("SpawnEnemy Fail");
            }

            yield return new WaitForSeconds(this.waveData.spawnBetweenDelay);
        }

        this.waveStatus = WAVE_STATUS.COMPLETE;

        //foreach (string e in this.waveData.enemyList)
        //{
        //    Transform newEnemy = EnemyManager.instance.SpawnEnemy(e, transform.position);
        //    newEnemy.gameObject.SetActive(true);
        //    MoveByPath newEnemyMoving = newEnemy.GetComponentInChildren<MoveByPath>();
        //    newEnemyMoving.LoadCheckPoints(this.path);
        //    newEnemyMoving.StartMoving();

        //    yield return new WaitForSeconds(this.waveData.spawnBetweenDelay);
        //}
    }

    public virtual void StartSpawning()
    {
        if (this.waveStatus == WAVE_STATUS.REARY_TO_SPAWN)
        {
            //this.LoadComponents();
            StartCoroutine(SpawnEnemy());
        }
    }

    public virtual void PauseSpawn(float pauseForSeconds = 0f)
    {
        StartCoroutine(PauseWaveForSeconds(pauseForSeconds));
    }

    public virtual void PauseSpawn()
    {
        this.PauseSpawn(0f);
    }

    public virtual void ResumeSpawn()
    {
        this.waveStatus = WAVE_STATUS.REARY_TO_SPAWN;
        this.StartSpawning();
    }

    public virtual void StopSpawning()
    {
        this.waveStatus = WAVE_STATUS.COMPLETE;
        DestroyImmediate(this);
    }

    protected virtual IEnumerator PauseWaveForSeconds(float sec)
    {
        this.waveStatus = WAVE_STATUS.PAUSE;
        if (sec != 0)
        {
            yield return new WaitForSeconds(sec);
            this.waveStatus = WAVE_STATUS.PAUSE;
        }
    }

    public virtual bool IsWaveComplete()
    {
        if (this.waveStatus == WAVE_STATUS.COMPLETE) return true;
        return false;
    }
}
