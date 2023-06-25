using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIngame : PMonoBehaviour
{
    [SerializeField] protected UIManager uiManager;
    [SerializeField] public Slider playerHPBar;
    [SerializeField] public Slider playerLevelBar;
    [SerializeField] public Slider playerSkillBarLeft;
    [SerializeField] public Slider playerSkillBarRight;
    [SerializeField] public Slider bossHPBar;
    [SerializeField] public Image playerCurrentSkillImage;
    [SerializeField] protected Transform skill;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIManager();
        this.LoadBossHPBar();
        this.LoadPlayerUI();
    }

    protected virtual void LoadPlayerUI()
    {
        this.skill = transform.Find("Skill");
        if (skill == null) return;

        this.LoadPlayerHPBar();
        this.LoadPlayerLeverBar();
        this.LoadPlayerCurrentSkillImage();
        this.LoadPlayerSpecialSkill();
    }

    protected virtual void LoadPlayerSpecialSkill()
    {
        this.playerSkillBarLeft = this.skill.Find("PlayerSkillLeft").GetComponent<Slider>();
        this.playerSkillBarRight = this.skill.Find("PlayerSkillRight").GetComponent<Slider>();
    }

    private void LoadPlayerCurrentSkillImage()
    {
        this.playerCurrentSkillImage = this.skill.Find("PlayerCurrentSkill").GetComponent<Image>();
    }

    protected virtual void LoadUIManager()
    {
        this.uiManager = transform.GetComponentInParent<UIManager>();
    }

    protected virtual void LoadPlayerHPBar()
    {
        this.playerHPBar = this.skill.Find("PlayerHPBar").GetComponent<Slider>();
    }

    protected virtual void LoadPlayerLeverBar()
    {
        this.playerLevelBar = this.skill.Find("PlayerLevelBar").GetComponent<Slider>();
    }

    protected override void Awake()
    {
        base.Awake();
        this.bossHPBar.gameObject.SetActive(false);
    }

    protected virtual void LoadBossHPBar()
    {
        this.bossHPBar = transform.Find("BossHPBar").GetComponent<Slider>();
    }

    public virtual void UpdatePlayerHPBar(float value)
    {
        this.playerHPBar.value = value;
    }

    public virtual void UpdateBossHPBar(float value)
    {
        this.bossHPBar.value = value;
    }

    public virtual void ActiveBossHPBar(float value)
    {
        this.bossHPBar.gameObject.SetActive(true);
        this.bossHPBar.value = value;
    }

    public virtual void DeactiveBossHPBar()
    {
        this.bossHPBar.gameObject.SetActive(false);
    }

    public virtual void PauseGame()
    {
        GameManager.instance.PauseGame();
    }

    public virtual void UpdatePlayerLevel(float value)
    {
        this.playerLevelBar.value = value;
    }

    public virtual void UpdatePlayerCurrentSkill(Sprite image)
    {
        this.playerCurrentSkillImage.sprite = image;
    }

    protected virtual void UpdateSpecialSkill(float value)
    {
        this.playerSkillBarLeft.value = value;
        this.playerSkillBarRight.value = value;
    }

    public virtual void UpdateSpecialSkill(float currentValue, float maxValue)
    {
        this.UpdateSpecialSkill((maxValue - currentValue) / maxValue);
    }

    protected override void OnEnable()
    {
        base.Start();
        PlayerCtrl.instance.RegisOnSkillCoolDown(this.UpdateSpecialSkill);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerCtrl.instance.UnRegisOnSkillCoolDown(this.UpdateSpecialSkill);
    }
}
