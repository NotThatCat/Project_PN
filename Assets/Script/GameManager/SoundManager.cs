using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : PMonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] protected AudioSource bulletSound;

    protected override void LoadComponents()
    {
        this.bulletSound = transform.GetComponent<AudioSource>();
    }


    protected override void Start()
    {
        if (!instance || instance != this)
        {
            instance = this;
        }
    }

    public virtual void PlaySound(string name)
    {
        bulletSound.Play(0);
    }
}
