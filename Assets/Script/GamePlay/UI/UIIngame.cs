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
    [SerializeField] public Slider bossHPBar;
    [SerializeField] public Image playerCurrentSkillImage;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIManager();
        this.LoadPlayerHPBar();
        this.LoadBossHPBar();
        this.LoadPlayerCurrentSkillImage();
    }

    private void LoadPlayerCurrentSkillImage()
    {
        this.playerCurrentSkillImage = transform.Find("PlayerCurrentSkill").GetComponent<Image>();
    }

    protected override void Awake()
    {
        base.Awake();
        this.bossHPBar.gameObject.SetActive(false);
    }

    protected virtual void LoadUIManager()
    {
        this.uiManager = transform.GetComponentInParent<UIManager>();
    }

    protected virtual void LoadPlayerHPBar()
    {
        this.playerHPBar = transform.Find("PlayerHPBar").GetComponent<Slider>();
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
}
