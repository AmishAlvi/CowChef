using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundLoop : MonoBehaviour
{
    public AudioClip clip;
    public float volume;

    public void PlaySoundClip()
    {
        AudioManager.instance.PlaySoundLoop(clip, volume);
    }
}
