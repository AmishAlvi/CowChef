using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    public void PlaySoundClip()
    {
        AudioManager.instance.PlaySound(clip);
    }
}
