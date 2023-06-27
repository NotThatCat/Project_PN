using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : PMonoBehaviour
{
    private static TimerManager _instance;

    public static TimerManager Instance => _instance;

    [SerializeField] private int _IDCounter = 0;
    [SerializeField] private int _IDs = 0;
    private Dictionary<int, Coroutine> lookups = new Dictionary<int, Coroutine>();

    protected override void Awake()
    {
        if (TimerManager._instance != this && TimerManager._instance != null) Debug.Log("Only allow one TimerManager");
        TimerManager._instance = this;
    }

    protected override void FixedUpdate()
    {
        this._IDs = this.lookups.Count;
    }

    protected override void Start()
    {
        if (!_instance || _instance != this)
        {
            _instance = this;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="time"></param>
    /// <param name="onCompleted"></param>
    /// <returns></returns>
    /// Learn this form <@338353730720235530>. +1 Respect
    public int StartTimer(float time, Action onCompleted)
    {
        return StartTimer(time, null, onCompleted);
    }

    public virtual int StartTimer(float time, Action<float> onUpdate, Action onCompleted)
    {
        _IDCounter++;
        lookups[_IDCounter] = StartCoroutine(StartTimer(_IDCounter, time, onUpdate, onCompleted));

        return _IDCounter;
    }

    protected virtual IEnumerator StartTimer(int timerID, float time, Action<float> onUpdate, Action onCompleted)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            onUpdate?.Invoke(time);
            yield return null;
        }
        lookups.Remove(timerID);
        onCompleted?.Invoke();
    }

    public virtual void StopTimer(int timerID)
    {
        if (this.lookups.ContainsKey(timerID)) StopCoroutine(this.lookups[timerID]);
    }
}
