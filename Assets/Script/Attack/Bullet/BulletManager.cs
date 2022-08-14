using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : PMonoBehaviour
{
    public static BulletManager instance;
    [SerializeField] public string holderName = "BulletHolder";
    [SerializeField] public Transform bulletHolder;
    [SerializeField] public List<Transform> bullets;

    protected override void Start()
    {
        LoadComponents();
        this.HideAll();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletHolder();
        this.LoadBullets();
    }

    protected override void Awake()
    {
        if (BulletManager.instance != this && BulletManager.instance != null) Debug.Log("Only allow one EffectManager");
        BulletManager.instance = this;
    }

    protected virtual void LoadBulletHolder()
    {
        this.bulletHolder = GameObject.Find(this.holderName).transform;
    }

    protected virtual void LoadBullets()
    {
        foreach (Transform bullet in transform)
        {
            this.bullets.Add(bullet);
        }
    }

    protected virtual void HideAll()
    {
        foreach (Transform bullet in this.bullets)
        {
            bullet.gameObject.SetActive(false);
        }
    }

    public virtual Transform Spawn(string bulletName, Vector3 spawnPosition)
    {
        Transform bulletPrefab = this.GetBulletByName(bulletName);
        Transform newBullet = Instantiate(bulletPrefab);
        newBullet.position = spawnPosition;
        newBullet.parent = this.bulletHolder;
        return newBullet;
    }

    public virtual Transform Spawn(string bulletName, Vector3 spawnPosition, Quaternion rotation)
    {
        Transform bulletPrefab = this.GetBulletByName(bulletName);
        Transform newBullet = Instantiate(bulletPrefab, spawnPosition, rotation, this.bulletHolder);
        return newBullet;
    }

    public virtual Transform GetBulletByName(string bulletName)
    {
        foreach (Transform bullet in this.bullets)
        {
            if (bullet.name == bulletName) return bullet;
        }
        return null;
    }

}
