using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseGame : PMonoBehaviour
{
    [SerializeField] protected Slider MusicSlider;
    [SerializeField] protected Slider SFXSlider;
    [SerializeField] protected Button ResumeBtn;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.LoadSliderValue();
    }

    protected virtual void LoadSliderValue()
    {
        this.MusicSlider.value = SoundManager.instance.GetMusicVolume();
        this.SFXSlider.value = SoundManager.instance.GetSFXVolume();
    }

    public virtual void UpdateMusicVolumn()
    {
        SoundManager.instance.UpdateMusicVolumn(this.MusicSlider.value);
    }

    public virtual void UpdateSFXVolumn()
    {
        SoundManager.instance.UpdateSFXVolumn(this.SFXSlider.value);
    }

    public virtual void ResumeGame()
    {
        GameManager.instance.ResumeGame();
    }
}
