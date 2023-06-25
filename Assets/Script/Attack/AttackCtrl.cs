using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackCtrl : PMonoBehaviour
{
    [SerializeField] protected List<Skill> skillCtrls;
    [SerializeField] protected int defaultSkill = 0;
    [SerializeField] protected int currentSkill;
    [SerializeField] protected bool toogleDefaultAttack = true;

    protected override void Awake()
    {
        if (this.defaultSkill >= this.skillCtrls.Count || this.defaultSkill < 0)
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
        this.skillCtrls = new List<Skill>();
        foreach (Transform skill in transform)
        {
            Skill skillProcessCtrl = skill.GetComponent<Skill>();
            if (skillProcessCtrl != null)
            {
                skillCtrls.Add(skill.GetComponent<Skill>());
            }
        }

        if (this.skillCtrls.Count <= 0) this.LogError("Skill not found");
    }

    /// <summary>
    /// Default Plane always attack
    /// </summary>
    protected override void Start()
    {
        base.FixedUpdate();
        this.DefaultAttack();
    }

    public virtual void DefaultAttack()
    {
        if (this.toogleDefaultAttack)
        {
            this.Attack();
            this.toogleDefaultAttack = false; /// break so that attack only happen once
        }
    }

    /// <summary>
    /// This is where attackCtrl interact with skillCtrl
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    protected virtual bool StartSkill(int idx)
    {
        return skillCtrls[idx].StartAttack();
    }

    protected virtual void StopSkill(int idx)
    {
        this.skillCtrls[idx].StopAttack();
    }

    /// <summary>
    /// Overide for other control attack style
    /// </summary>
    /// <param name="idx"></param>
    public virtual bool Attack()
    {
        return this.StartSkill(this.currentSkill);
    }

    /// <summary>
    /// Overide for other control attack style
    /// </summary>
    /// <param name="idx"></param>
    public virtual bool Attack(int idx)
    {
        return this.StartSkill(idx);
    }

    public virtual bool Attack(string skillName)
    {
        int skillIdx = this.GetSkillIndexByName(skillName);
        if (skillIdx >= 0) return this.StartSkill(skillIdx);
        return false;
    }


    public virtual int ChangeSkill(int idx)
    {
        if (idx >= this.skillCtrls.Count || idx < 0) return this.currentSkill;
        this.StopSkill(this.currentSkill);
        return this.currentSkill = idx;
    }

    public virtual void NextSkill(int value)
    {
        this.ChangeSkill(this.currentSkill + value);
    }

    public virtual void ChangeSkill(string skillName)
    {
        int skillIdx = this.GetSkillIndexByName(skillName);
        if (skillIdx >= 0) ChangeSkill(skillIdx);
    }

    public virtual Sprite GetCurrentSkillImage()
    {
        return this.GetCurrentSkill().GetSkillImage();
    }

    public virtual Skill GetCurrentSkill()
    {
        return this.skillCtrls[this.currentSkill];
    }

    public virtual void StopAllSkill()
    {
        foreach (Skill skill in this.skillCtrls)
        {
            this.StopSkill(skill);
        }
    }

    public virtual void StopAllSkillType(SKILL_TYPE skillType = SKILL_TYPE.DEFAULT)
    {
        foreach (Skill sk in this.skillCtrls)
        {
            sk.StopSkillType(skillType);
        }
    }

    public virtual void StopSkill(Skill skill)
    {
        skill.StopAttack();
    }

    protected virtual int GetSkillIndexByName(string skillName)
    {
        int skillIdx = -1;
        for (int i = 0; i < this.skillCtrls.Count; i++)
        {
            if (skillCtrls[i].name != skillName) continue;
            skillIdx = i;
        }

        if (skillIdx < 0) Debug.Log(skillName = " Not Found");

        return skillIdx;
    }

    public abstract int GetMaxLevel();
    public abstract int GetCurrentLevel();

    public override void ResetValue()
    {
        base.ResetValue();
        foreach (Skill skill in this.skillCtrls) { skill.ResetValue(); }
        this.defaultSkill = this.GetDefaultSkill();
        this.currentSkill = this.defaultSkill;
        this.toogleDefaultAttack = true;
    }

    protected abstract int GetDefaultSkill();
}
