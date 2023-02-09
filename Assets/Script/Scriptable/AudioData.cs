using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Scriptable/SoundData", order = 1)]
public class AudioData : ScriptableObject
{
    [SerializeField] public List<Sound> listSound;

    public virtual AudioClip GetClip(SoundID soundID)
    {
        foreach (Sound sound in listSound)
        {
            if (sound.soundID == soundID) return sound.soundClip;
        }
        return null;
    }
}