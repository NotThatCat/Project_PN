using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyArmy : Skill
{
    [SerializeField] private FormationBase _formation;

    public FormationBase Formation
    {
        get
        {
            if (_formation == null) this.LoadFormation();
            return _formation;
        }
        set => _formation = value;
    }

    [SerializeField] protected GameObject _unitPrefab;

    //[SerializeField] protected List<GameObject> _minions = new List<GameObject>();
    [SerializeField] protected string _holderName = "Holder";
    [SerializeField] protected Transform _holder;
    //[SerializeField] protected List<GameObject> _minionPos = new List<GameObject>();
    [SerializeField] protected List<Vector3> _points = new List<Vector3>();

    [SerializeField] private Dictionary<int, Minion> _minionList = new Dictionary<int, Minion>();
    [SerializeField] protected int totalMinions = 0;

    [SerializeField] protected bool _isSpawned = false;
    [SerializeField] protected AutoSpin _autoSpin;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHolder();
        this.LoadFormation();
        this.SetSpiner();
    }

    protected virtual void SetSpiner()
    {
        this._autoSpin = transform.GetComponentInChildren<AutoSpin>();
        if (this._autoSpin != null && this._holder != null)
        {
            this._autoSpin.model = this._holder;
        }
    }

    protected virtual void LoadHolder()
    {
        this._holder = transform.Find(this._holderName);
    }

    protected virtual void LoadFormation()
    {
        if (_formation == null) _formation = GetComponentInChildren<FormationBase>();

        this._points = Formation.EvaluatePoints().ToList();
        //if (this._minionPos.Count <= 0)
        //{
        //    this._minionPos = new List<GameObject>();
        //    foreach (Vector3 pos in this._points)
        //    {
        //        GameObject minionPos = Instantiate(new GameObject(), this._holder);
        //        minionPos.transform.SetPositionAndRotation(pos + transform.position, transform.rotation);
        //        this._minionPos.Add(minionPos);
        //    }
        //}

        if (this._minionList.Count <= 0)
        {
            this._minionList = new Dictionary<int, Minion>();
            for (int i = 0; i < this._points.Count; i++)
            {
                GameObject minionPos = Instantiate(new GameObject(), this._holder);
                minionPos.transform.SetPositionAndRotation(_points[i] + transform.position, transform.rotation);

                Minion minion = new Minion(minionPos, null);
                this._minionList.Add(i, minion);
            }
        }
    }

    protected override void Update()
    {
        //if (this._minions.Count > 0)
        //{
        //    this.MoveMinions(this._minionPos);
        //}

        if (this.totalMinions > 0)
        {
            this.MoveMinions();
        }
    }

    //protected virtual void SetFormation()
    //{
    //    if (_points.Count > _minions.Count)
    //    {
    //        var remainingPoints = _points.Skip(_minions.Count);
    //        if (!this._isSpawned)
    //        {
    //            Spawn(remainingPoints);
    //        }
    //    }
    //    else if (_points.Count < _minions.Count)
    //    {
    //        Kill(_minions.Count - _points.Count);
    //    }

    //    //this.MoveMinions(this._minionPos);
    //}

    protected virtual void Spawn(IEnumerable<Vector3> points)
    {
        this._isSpawned = true;
        foreach (var pos in points)
        {
            this.Spawn();
        }
    }

    protected virtual Transform Spawn()
    {
        Transform unit = EnemySpawner.Instance.Spawn(_unitPrefab.name, transform.position);
        unit.gameObject.SetActive(true);
        unit.GetComponent<EnemyCtrl>()?.StartDefaultAttack();
        //_minions.Add(unit.gameObject);
        return unit;
    }

    //protected virtual void Kill(int num)
    //{
    //    for (var i = 0; i < num; i++)
    //    {
    //        var unit = _minions.Last();
    //        _minions.Remove(unit);
    //        EnemySpawner.Instance.Despawn(unit.transform);
    //    }
    //}

    //protected virtual void MoveMinions(List<GameObject> point)
    //{
    //    for (var i = 0; i < _minions.Count; i++)
    //    {
    //        if (_minions[i].activeSelf)
    //        {
    //            _minions[i].transform.position = Vector3.MoveTowards(_minions[i].transform.position, point[i].transform.position, this.GetUnitSpeed(_minions[i]) * Time.deltaTime);
    //        }
    //    }
    //}

    protected virtual void MoveMinions()
    {
        for (var i = 0; i < _minionList.Count; i++)
        {
            if (this._minionList[i].minion != null && this._minionList[i].minion.activeSelf != false)
            {
                Transform pos = _minionList[i].posHolder.transform;
                Transform mn = _minionList[i].minion.transform;
                mn.position = Vector3.MoveTowards(mn.position, pos.position, this.GetUnitSpeed(mn) * Time.deltaTime);
            }
        }
    }

    protected virtual float GetUnitSpeed(GameObject unit)
    {
        EnemyCtrl unitCtrl = unit.GetComponentInChildren<EnemyCtrl>();
        return unitCtrl.enemyMoving.movingSpeed;
    }

    protected virtual float GetUnitSpeed(Transform unit)
    {
        EnemyCtrl unitCtrl = unit.GetComponentInChildren<EnemyCtrl>();
        return unitCtrl.enemyMoving.movingSpeed;
    }

    protected override void MainAttack()
    {
        if (_minionList.Count <= 0) this.LoadFormation();

        this.ReloadFormation();
        for (var i = 0; i < _minionList.Count; i++)
        {
            if (this._minionList[i].minion == null)
            {
                Transform newMinion = this.Spawn();
                this._minionList[i].minion = newMinion.gameObject;
                totalMinions++;
            }
        }
    }

    protected virtual void ReloadFormation()
    {
        for (var i = 0; i < _minionList.Count; i++)
        {
            if (this._minionList[i].minion == null) return;
            if (this._minionList[i].minion.activeSelf == false)
            {
                this._minionList[i].minion = null;
                totalMinions--;
            }
        }
    }
}