using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private float volume;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlaySound(clip, volume);
    }

}
