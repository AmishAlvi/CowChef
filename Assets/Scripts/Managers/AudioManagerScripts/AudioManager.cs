using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //static variable holds singleton instance
    public static AudioManager instance;

    [SerializeField] private AudioSource musicSource, effectSource, loopingEffectSource;

    //making sure there is only ever 1 instance of this script
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip audioClip, float volume)
    {
        effectSource.PlayOneShot(audioClip, volume);
    }

    public void PlaySoundLoop(AudioClip audioClip, float volume)
    {
        loopingEffectSource.loop = true;
        loopingEffectSource.PlayOneShot(audioClip,volume);
    }

    public void ChangeMusic(AudioClip clip, float volume)
    {
        //musicSource.clip = clip;
       // musicSource.volume = volume;
        musicSource.PlayOneShot(clip, volume);
    }

    public void ToggleEffects()
    {
        effectSource.mute = !effectSource.mute;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void StopEffect()
    {
        effectSource.Stop();
    }

    public void StopAllAudio()
    {
        StopMusic();
        StopEffect();
    }
}
