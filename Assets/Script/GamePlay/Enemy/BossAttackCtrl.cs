using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackCtrl : AttackCtrl
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    //[SerializeField] protected BossData bossData;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
        //this.LoadBossData();
        //if (this.bossData != null)
        //{
        //    this.LoadMovingSkill();
        //    if (this.bossData.randomAttack)
        //    {
        //        this.GenerateAttackProcess();
        //    }
        //    if (this.bossData.bossCanMoving && this.bossData.randomMoving)
        //    {
        //        this.GenerateMovingProcess();
        //    }
        //}
        //else
        //{
        //    Debug.Log("Cannot Load bossData");
        //    this.toogleDefaultAttack = false;
        //}
    }

    //protected virtual void LoadBossData()
    //{
    //    this.bossData = this.enemyCtrl.GetBossData();
    //}

    //protected virtual void GenerateMovingProcess()
    //{
    //    for (int i = 0; i < this.bossData.maxMovingTime; i++)
    //    {
    //        int nextProcess = Random.Range(0, this.skillCtrls.Count);
    //        if (this.bossData.bossCanMoving)
    //        {
    //            while (nextProcess == this.bossData.movingSkill)
    //            {
    //                nextProcess = Random.Range(0, this.skillCtrls.Count);
    //            }
    //        }
    //        this.bossData.attackProcess.Add(nextProcess);
    //    }
    //}

    //protected virtual void GenerateAttackProcess()
    //{
    //    this.bossData.currentProcess = 0;
    //    this.bossData.attackProcess = new List<int>();
    //    for (int i = 0; i < this.bossData.maxProcess; i++)
    //    {
    //        int nextProcess = Random.Range(0, this.skillCtrls.Count);
    //        if (this.bossData.bossCanMoving)
    //        {
    //            while (nextProcess == this.bossData.movingSkill)
    //            {
    //                nextProcess = Random.Range(0, this.skillCtrls.Count);
    //            }
    //        }
    //        this.bossData.attackProcess.Add(nextProcess);
    //    }
    //}

    protected virtual void LoadEnemyCtrl()
    {
        this.enemyCtrl = transform.GetComponentInParent<EnemyCtrl>();
    }

    //protected virtual void LoadMovingSkill()
    //{
    //    if (this.bossData.bossCanMoving)
    //    {
    //        for (int i = 0; i < skills.Count; i++)
    //        {
    //            if (skills[i].name == this.bossData.movingSkillName)
    //            {
    //                this.bossData.movingSkill = i;
    //                break;
    //            }
    //        }
    //        if (this.bossData.movingSkill == -1)
    //        {
    //            Debug.Log("Cannot find moving skill, disabling moving ability");
    //            this.bossData.bossCanMoving = false;
    //        }
    //    }
    //}

    //protected override void FixedUpdate()
    //{

    //}

    //protected override void Awake()
    //{
    //    //this.LoadComponents();
    //    base.Awake();
    //    if (!this.toogleDefaultAttack)
    //    {
    //        return;
    //    }
    //    if (this.bossData.attackProcess.Count != 0)
    //    {
    //        StartCoroutine(AttackProcess());
    //    }
    //}

    //protected virtual void RemoveSkillOutOfProcess(int idx)
    //{
    //    for (int i = 0; i < this.bossData.attackProcess.Count; i++)
    //    {
    //        if (i == idx)
    //        {
    //            this.bossData.attackProcess.RemoveAt(i);
    //            i--;
    //        }
    //    }
    //}

    //protected virtual IEnumerator AttackProcess()
    //{
    //    while (this.bossData.currentProcess < this.bossData.maxProcess && this.bossData.currentProcess < this.bossData.attackProcess.Count)
    //    {
    //        this.currentSkill = this.bossData.attackProcess[this.bossData.currentProcess];
    //        float delay = this.skillCtrls[this.currentSkill].GetDelay();
    //        this.skillCtrls[this.currentSkill].StartAttack();
    //        //yield return new WaitForSeconds(delay);
    //        yield return new WaitForSeconds(1f);
    //        this.bossData.currentProcess++;
    //    }
    //}

    public override int GetMaxLevel()
    {
        return this.enemyCtrl.GetMaxLevel();
    }

    public override int GetCurrentLevel()
    {
        return this.enemyCtrl.GetCurrentLevel();
    }
}
