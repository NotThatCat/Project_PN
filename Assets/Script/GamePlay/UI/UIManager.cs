using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : PMonoBehaviour
{
    public static UIManager instance;

    [SerializeField] protected List<Transform> uiList;

    [SerializeField] protected Transform mainUI;
    [SerializeField] protected Transform pauseUI;
    [SerializeField] protected Transform inGameUI;
    [SerializeField] protected Transform settingUI;
    //[SerializeField] protected Transform garageUI;

    [SerializeField] protected UIIngame uiIngame;
    [SerializeField] protected UIPauseGame uiPauseGame;
    [SerializeField] protected UIMenu uiMenu;
    [SerializeField] protected UISetting uiSetting;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUI();
        //this.LoadUIIngame();
    }

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    protected virtual void LoadUI()
    {
        this.mainUI = transform.Find("MainUI");
        this.pauseUI = transform.Find("PauseUI");
        this.inGameUI = transform.Find("InGameUI");
        this.settingUI = transform.Find("SettingUI");

        this.uiIngame = transform.GetComponentInChildren<UIIngame>();
        this.uiPauseGame = transform.GetComponentInChildren<UIPauseGame>();
        this.uiMenu = transform.GetComponentInChildren<UIMenu>();
        this.uiSetting = transform.GetComponentInChildren<UISetting>();


        this.uiList = new List<Transform>();
        foreach (Transform ui in transform)
        {
            this.uiList.Add(ui);
        }
    }

    //protected virtual void LoadUIIngame()
    //{
    //    this.uiIngame = transform.GetComponentInChildren<UIIngame>();
    //}

    public virtual void UpdatePlayerHPBar(float value)
    {
        this.uiIngame.UpdatePlayerHPBar(value);
    }

    public virtual void UpdateBossHPBar(float value)
    {
        this.uiIngame.UpdateBossHPBar(value);
    }

    public virtual void ActiveBossHPBar(float value)
    {
        this.uiIngame.ActiveBossHPBar(value);
    }

    public virtual void DeactiveBossHPBar()
    {
        this.uiIngame.DeactiveBossHPBar();
    }

    public virtual void HidaAll()
    {
        foreach(Transform ui in this.uiList)
        {
            ui.gameObject.SetActive(false);
        }
    }

    public virtual void ShowUIPauseGame()
    {
        this.HidaAll();
        this.uiPauseGame.gameObject.SetActive(true);
    }

    public virtual void ShowUIIngame()
    {
        this.HidaAll();
        this.uiIngame.gameObject.SetActive(true);
    }

    public virtual void ShowUISetting()
    {
        this.HidaAll();
        this.uiSetting.gameObject.SetActive(true);
    }

    public virtual void ShowUIMenu()
    {
        this.HidaAll();
        this.uiMenu.gameObject.SetActive(true);
    }

    public virtual void UpdatePlayerLevel(float value)
    {
        this.uiIngame.UpdatePlayerLevel(value);
    }

    public virtual void UpdatePlayerCurrentSkill(Sprite image)
    {
        this.uiIngame.UpdatePlayerCurrentSkill(image);
    }
}
