using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : PMonoBehaviour
{
    public static UIManager instance;

    [SerializeField] protected List<Transform> uiList;
    [SerializeField] protected Slider playerHPBar;
    [SerializeField] protected Slider BossHPBar;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIList();
        this.LoadPlayerHPBar();
        this.LoadBossHPBar();
    }

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
    }

    protected virtual void LoadUIList()
    {
        this.uiList = new List<Transform>();
        foreach (Transform ui in transform)
        {
            this.uiList.Add(ui);
        }
    }

    protected virtual void LoadPlayerHPBar()
    {
        this.playerHPBar = transform.Find("PlayerHPBar").GetComponent<Slider>();
    }

    protected virtual void LoadBossHPBar()
    {
        this.BossHPBar = transform.Find("BossHPBar").GetComponent<Slider>();
    }

    public virtual void UpdatePlayerHPBar(float value)
    {
        this.playerHPBar.value = value;
    }

    public virtual void UpdateBossHPBar(float value)
    {
        this.BossHPBar.value = value;
    }

    public virtual void ActiveBossHPBar(float value)
    {
        this.BossHPBar.gameObject.SetActive(true);
        this.BossHPBar.value = value;
    }

    public virtual void DeactiveBossHPBar()
    {
        this.BossHPBar.gameObject.SetActive(false);
    }
}
