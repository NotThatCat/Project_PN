using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : PMonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] protected AudioSource ASMusic;
    [SerializeField] protected AudioSource ASSFX;

    [SerializeField] protected List<AudioData> listSound;
    [SerializeField] protected AudioData soundMusic;
    [SerializeField] protected AudioData soundSFX;

    [SerializeField] protected bool mute = false;

    protected override void LoadComponents()
    {
        this.LoadSoundData();
        this.LoadSoundSource();
    }

    protected virtual void LoadSoundData()
    {
        if (this.listSound.Count > 0)
        {
            this.listSound = new List<AudioData>();
        }

        AudioData[] soundDatas = Resources.FindObjectsOfTypeAll(typeof(AudioData)) as AudioData[];

        foreach (AudioData sound in soundDatas)
        {
            if (sound.name == "SoundMusic")
                this.soundMusic = sound;
            if (sound.name == "SoundSFX")
                this.soundSFX = sound;

            this.listSound.Add(sound);
        }
    }

    protected virtual void LoadSoundSource()
    {
        this.ASMusic = transform.Find("Music").GetComponent<AudioSource>();
        this.ASSFX = transform.Find("SFX").GetComponent<AudioSource>();
    }


    protected override void Start()
    {
        if (!instance || instance != this)
        {
            instance = this;
        }

        //if (!this.mute)
        //{
        //    this.themeSound.PlayOneShot(this.themeSound.clip);
        //}
    }

    public virtual void PlaySound(SoundID id)
    {
        this.PlayMusic(id);
        this.PlaySFX(id);
    }

    public virtual void PlaySFX(SoundID id)
    {
        AudioClip sound = this.soundSFX.GetClip(id);
        if (sound != null && !this.mute)
        {
            this.ASSFX.PlayOneShot(sound);
        }
    }

    public virtual void PlayMusic(SoundID id)
    {
        AudioClip sound = this.soundMusic.GetClip(id);
        if (sound != null && !this.mute)
        {
            //this.ASMusic.PlayOneShot(sound);
            this.ASMusic.clip = sound;
            this.ASMusic.Play();
        }
    }

    public virtual void StopAllSFX()
    {
        this.ASSFX.Stop();
    }

    public virtual void StopAllMusic()
    {
        this.ASMusic.Stop();
    }

    public virtual void MuteALlSound()
    {
        this.mute = true;
    }

    public virtual void UnMute()
    {
        this.mute = false;
    }

    public virtual void UpdateMusicVolumn(float value)
    {
        this.ASMusic.volume = value;
    }

    public virtual void UpdateSFXVolumn(float value)
    {
        this.ASSFX.volume = value;
    }

    public virtual float GetMusicVolume()
    {
        return this.ASMusic.volume;
    }

    public virtual float GetSFXVolume()
    {
        return this.ASSFX.volume;
    }

    public virtual void PauseMusic()
    {
        this.ASMusic.Pause();
    }

    public virtual void ResumeMusic()
    {

        this.ASMusic.Play();
    }
}
