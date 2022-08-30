using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : PMonoBehaviour
{
    [SerializeField] protected List<SkillSO> skillDatas;

    public static SkillManager instance;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSkillData();
    }

    protected override void Awake()
    {
        if (!instance || instance != this)
        {
            instance = this;
        }
    }

    protected virtual void LoadSkillData()
    {
        if (this.skillDatas.Count > 0)
        {
            this.skillDatas = new List<SkillSO>();
        }

        SkillSO[] skills = Resources.FindObjectsOfTypeAll(typeof(SkillSO)) as SkillSO[];

        foreach (SkillSO skill in skills)
        {
            this.skillDatas.Add(skill);
        }
    }

    public virtual SkillSO GetSkillData(string name)
    {
        foreach (SkillSO skill in this.skillDatas)
        {
            if (skill.name == name) return skill;
        }

        return null;
    }
}
