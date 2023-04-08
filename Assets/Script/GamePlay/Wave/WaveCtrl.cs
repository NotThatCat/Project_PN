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
        this.LoadWaveManager();
    }

    protected virtual void LoadWaveManager()
    {
        this.waveManager = WaveManager.instance;
        //this.waveManager = transform.parent.parent.GetComponent<WaveManagerTest>();
    }

    protected virtual void LoadWaveInfo(WAVE_ID id)
    {
        if (this.waveData || this.waveData == null)
        {
            this.waveData = this.waveManager.GetWaveDataByID(id);
        }
    }

    protected virtual void LoadPath()
    {
        //this.path = EnemyManager.instance.GetMovingPath(this.waveData.pathName);
        this.path = GameObject.Find(this.waveData.pathName).transform;
    }

    public virtual void CreateWave(WAVE_ID id)
    {
        this.LoadComponents();
        this.LoadWaveInfo(id);
        this.LoadPath();
    }

    public virtual void StartSpawning()
    {
        if (this.waveStatus == WAVE_STATUS.REARY_TO_SPAWN)
        {
            StartCoroutine(SpawnEnemy());
        }
        else
        {
            Debug.Log("Wave in " + this.waveStatus);
        }
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

    protected virtual IEnumerator SpawnEnemy()
    {
        this.waveStatus = WAVE_STATUS.SPAWNING;
        for (int i = this.currentIndex; i < this.waveData.enemyList.Count; i++)
        {
            Vector3 spawnPosition = this.path.GetChild(0).position;
            //Transform newEnemy = EnemyManager.instance.SpawnEnemy(this.waveData.enemyList[i], spawnPosition);
            Transform newEnemy = EnemySpawner.Instance.Spawn(this.waveData.enemyList[i], spawnPosition, transform.rotation);
            if (newEnemy != null)
            {
                MoveByPath newEnemyMoving = newEnemy.GetComponentInChildren<MoveByPath>();
                newEnemyMoving.LoadCheckPoints(this.path);
                newEnemy.gameObject.SetActive(true);
                newEnemy.GetComponent<EnemyCtrl>()?.StartDefaultAttack();
                newEnemyMoving.StartMoving();
            }
            else
            {
                Debug.Log("SpawnEnemy Fail");
            }

            yield return new WaitForSeconds(this.waveData.spawnBetweenDelay);
        }

        this.waveStatus = WAVE_STATUS.COMPLETE;
    }

    public virtual bool IsWaveComplete()
    {
        if (this.waveStatus == WAVE_STATUS.COMPLETE) return true;
        return false;
    }
}
