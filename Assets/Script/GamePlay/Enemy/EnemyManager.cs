using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : PMonoBehaviour
{
    [SerializeField] protected string enemyListName = "ListEnemy";
    [SerializeField] protected Transform listEnemy;
    [SerializeField] protected List<Transform> enemyList;
    [SerializeField] protected string enemyHolderName = "EnemyHolder";
    [SerializeField] protected Transform enemyHolder;

    [SerializeField] protected string enemyMovingPathName = "EnemyMovingPath";
    [SerializeField] protected List<Transform> enemyMovingPath;

    public static EnemyManager instance;

    protected override void Awake()
    {
        // base.Awake();
        if (!instance || instance != this)
        {
            instance = this;
        }
    }

    protected override void Start()
    {

    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyList();
        this.LoadEnemyHolder();
        this.LoadEnemyMovingPath();
    }


    protected virtual void LoadEnemyMovingPath()
    {
        Transform enemyPaths = GameObject.Find(this.enemyMovingPathName).transform;
        foreach (Transform path in enemyPaths)
        {
            this.enemyMovingPath.Add(path);
        }
    }

    protected virtual void LoadEnemyHolder()
    {
        this.enemyHolder = GameObject.Find("EnemyHolder").transform;
    }

    protected virtual void LoadEnemyList()
    {
        this.listEnemy = transform.Find(this.enemyListName);
        this.enemyList = new List<Transform>();
        foreach (Transform e in this.listEnemy)
        {
            e.gameObject.SetActive(false);
            this.enemyList.Add(e);
        }
    }

    public Transform SpawnEnemy(string enemyName, Vector3 position)
    {
        Transform enemyPref = this.GetEnemyByName(enemyName);
        if (enemyPref != null)
        {
            Transform newEnemy = GameObject.Instantiate(enemyPref, position, transform.rotation, this.enemyHolder);
            newEnemy.gameObject.SetActive(true);
            return newEnemy;
        }

        return null;
    }

    protected virtual Transform GetEnemyByName(string enemyName)
    {
        foreach (Transform e in this.enemyList)
        {
            if (e.name == enemyName) return e;
        }
        return null;
    }

    public virtual Transform GetMovingPath(string name)
    {
        foreach (Transform path in this.enemyMovingPath)
        {
            if (path.name == name)
            {
                return path;
            }
        }
        return null;
    }
}
