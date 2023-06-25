using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCtrl : PMonoBehaviour
{
    [SerializeField] public WaveData waveData;
    [SerializeField] public WAVE_ID waveID;
    [SerializeField] protected Transform path;
    [SerializeField] protected WAVE_STATUS waveStatus = WAVE_STATUS.REARY_TO_SPAWN;
    [SerializeField] protected int currentIndex;

    private Coroutine currentSpawnEnemy;

    public virtual void ResetWave()
    {
        this.Reset();
    }

    protected override void LoadComponents()
    {

    }


    protected virtual void LoadWaveInfo(WAVE_ID id)
    {
        if (this.waveData || this.waveData == null)
        {
            this.waveData = WaveManager.instance.GetWaveDataByID(id);
        }
    }

    protected virtual void LoadPath()
    {
        //this.path = EnemyManager.instance.GetMovingPath(this.waveData.pathName);
        this.path = GameObject.Find(this.waveData.pathName).transform;
    }

    public virtual void CreateWave(WAVE_ID id)
    {
        this.waveID = id;
        this.LoadComponents();
        this.LoadWaveInfo(id);
        this.LoadPath();
    }

    protected override void Reset()
    {
        if (this.currentSpawnEnemy != null)
        {
            StopAllCoroutines();
        }
        this.currentIndex = 0;
        this.waveStatus = WAVE_STATUS.REARY_TO_SPAWN;
    }

    public virtual void StartSpawning()
    {
        if (this.waveStatus == WAVE_STATUS.REARY_TO_SPAWN)
        {
            StartCoroutine(StartSpawnEnemy());
        }
        else
        {
            Debug.Log("Wave in " + this.waveStatus);
        }
    }


    protected virtual IEnumerator StartSpawnEnemy()
    {
        this.waveStatus = WAVE_STATUS.SPAWNING;
        for (int i = this.currentIndex; i < this.waveData.enemyList.Count; i++)
        {
            this.SpawnEnemy(this.waveData.enemyList[i]);
            yield return new WaitForSeconds(this.waveData.spawnBetweenDelay);
        }

        this.waveStatus = WAVE_STATUS.COMPLETE;

        WaveManager.instance.DeSpawnWave(this.waveID);
    }

    protected virtual void SpawnEnemy(string enemyName)
    {
        Vector3 spawnPosition = this.path.GetChild(0).position;
        //Transform newEnemy = EnemyManager.instance.SpawnEnemy(this.waveData.enemyList[i], spawnPosition);
        Transform newEnemy = EnemySpawner.Instance.Spawn(enemyName, spawnPosition, transform.rotation);
        if (newEnemy != null)
        {
            MoveByPath newEnemyMoving = newEnemy.GetComponentInChildren<MoveByPath>();
            if (newEnemyMoving != null)
            {
                switch (newEnemyMoving.type)
                {
                    case MOVING_TYPE.PATH:
                        newEnemyMoving.LoadCheckPoints(this.path);
                        newEnemy.gameObject.SetActive(true);
                        newEnemy.GetComponent<EnemyCtrl>()?.StartDefaultAttack();
                        newEnemyMoving.StartMoving();
                        break;

                    case MOVING_TYPE.FOLLOW_TARGET:

                        break;

                    case MOVING_TYPE.ACC:

                        break;

                    default:
                        break;
                }
            }
        }
        else
        {
            Debug.Log("SpawnEnemy Fail");
        }
    }
}
