using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGoundManager : PMonoBehaviour
{
    public static BackGoundManager instance;

    [Header("Back Ground Info")]
    [SerializeField] protected string bgMainName = "M";
    [SerializeField] protected string bgLayer1Name = "L1";
    [SerializeField] protected string bgLayer2Name = "L2";
    [SerializeField] protected string holderName = "BG_1";
    [SerializeField] protected int selectedBG = 0;
    [SerializeField] protected List<Transform> bgMain;
    [SerializeField] protected List<Transform> bgLayer1;
    [SerializeField] protected List<Transform> bgLayer2;
    [SerializeField] protected Transform bgHolder;
    [SerializeField] protected float size = 16;

    [Header("Move Speed")]
    [SerializeField] protected float bgMoveSpeed = -3f;
    [SerializeField] protected float parallaxMain = .2f;
    [SerializeField] protected float parallaxL1 = .5f;
    [SerializeField] protected float parallaxL2 = .05f;
    [SerializeField] protected float parallaxX = 1.1f;

    public virtual void InitBG()
    {
        ClearHolder();
        Transform bgMPrefab = GetRandomBG(this.bgMain);
        Transform bgM = Instantiate(bgMPrefab, new Vector3(0, 0, 0), bgMPrefab.rotation, this.bgHolder);

        Transform bgL1Prefab = GetRandomBG(this.bgLayer1);
        Transform bgL1 = Instantiate(bgL1Prefab, new Vector3(0, 0, 0), bgL1Prefab.rotation, this.bgHolder);

        Transform bgL2Prefab = GetRandomBG(this.bgLayer2);
        Transform bgL2 = Instantiate(bgL2Prefab, new Vector3(0, 0, 0), bgL2Prefab.rotation, this.bgHolder);

        bgM.GetComponent<ParallaxBG>().UpdateParallax(this.parallaxMain, this.parallaxX);
        bgL1.GetComponent<ParallaxBG>().UpdateParallax(this.parallaxL1, this.parallaxX);
        bgL2.GetComponent<ParallaxBG>().UpdateParallax(this.parallaxL2, this.parallaxX);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMainBG();
        this.LoadLayer1();
        this.LoadLayer2();
        this.LoadBGHolder();
        this.HideAll();
        this.InitBG();
    }

    protected virtual void LoadMainBG()
    {
        Transform bgList = this.transform.Find(this.bgMainName);
        foreach (Transform item in bgList)
        {
            this.bgMain.Add(item);
        }
    }

    protected virtual void LoadLayer1()
    {
        Transform bgList = this.transform.Find(this.bgLayer1Name);
        foreach (Transform item in bgList)
        {
            this.bgLayer1.Add(item);
        }
    }

    protected virtual void LoadLayer2()
    {
        Transform bgList = this.transform.Find(this.bgLayer2Name);
        foreach (Transform item in bgList)
        {
            this.bgLayer2.Add(item);
        }
    }

    protected virtual void LoadBGHolder()
    {
        this.bgHolder = this.transform.Find(this.holderName);
    }

    protected virtual Transform GetRandomBG(List<Transform> list)
    {
        int idx = 0;
        int count = list.Count;
        if (count > 1)
        {
            idx = Random.Range(0, count - 1);
        }
        return list[idx];
    }

    protected virtual void ClearHolder()
    {
        foreach (Transform item in bgHolder)
        {
            Destroy(item.gameObject);
        }
    }

    protected virtual void HideAll()
    {
        this.bgMain[0].parent.gameObject.SetActive(false);
        this.bgLayer1[0].parent.gameObject.SetActive(false);
        this.bgLayer2[0].parent.gameObject.SetActive(false);
    }

    protected override void Update()
    {
        if (BackGoundManager.instance != this && BackGoundManager.instance != null) Debug.Log("Only allow one InputsManager");
        BackGoundManager.instance = this;
    }
}
