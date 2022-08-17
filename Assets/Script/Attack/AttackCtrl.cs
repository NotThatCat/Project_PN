using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCtrl : PMonoBehaviour
{
    [SerializeField] public List<Transform> skills;
    [SerializeField] public List<SkillCtrl> skillCtrls;
    [SerializeField] protected int defaultSkill = 0;
    [SerializeField] protected int currentSkill;
    [SerializeField] protected bool toogleDefaultAttack = true;

    protected override void Awake()
    {
        if (this.defaultSkill > this.skillCtrls.Count || this.defaultSkill < 0)
        {
            this.LogError("defaultSkill invalid");
            this.defaultSkill = 0;
        }
        this.currentSkill = this.defaultSkill;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSkills();
    }

    protected virtual void LoadSkills()
    {
        foreach (Transform skill in transform)
        {
            skills.Add(skill);
            skillCtrls.Add(skill.GetComponent<SkillCtrl>());
        }

        if (this.skills.Count <= 0) this.LogError("Skill not found");
        if (this.skillCtrls.Count <= 0) this.LogError("SkillCtrl not found");
    }

    /// <summary>
    /// This is where attackCtrl interact with skillCtrl
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    protected virtual bool UseSkill(int idx)
    {
        if (this.currentSkill != idx)
        {
            this.StopSkill(this.currentSkill);
        }
        return skillCtrls[idx].StartAttack();
    }

    protected virtual void StopSkill(int idx)
    {
        this.skillCtrls[idx].StopAttack();
    }

    /// <summary>
    /// Default Plane always attack
    /// </summary>
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (this.toogleDefaultAttack)
        {
            this.Attack();
            this.toogleDefaultAttack = false; /// break so that attack only happen once
        }
    }

    /// <summary>
    /// Overide for other control attack style
    /// </summary>
    /// <param name="idx"></param>
    public virtual bool Attack()
    {
        return this.UseSkill(this.currentSkill);
    }

    /// <summary>
    /// Overide for other control attack style
    /// </summary>
    /// <param name="idx"></param>
    public virtual bool Attack(int idx)
    {
        return this.UseSkill(idx);
    }


    public virtual int ChangeSkill(int idx)
    {
        if (idx >= this.skills.Count) return this.currentSkill;
        this.StopSkill(this.currentSkill);
        Debug.Log("Skill change to " + skills[idx].name);
        return this.currentSkill = idx;
    }
}
