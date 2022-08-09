using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    [SerializeField] AudioClip musicClip;
    [SerializeField] float volume;
    public void TriggerChangeMusic()
    {
        AudioManager.instance.ChangeMusic(musicClip, volume);
    }
}
