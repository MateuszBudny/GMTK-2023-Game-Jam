using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : DontDestroySingleBehaviour<SoundManager>
{
    [SerializeField]
    private AudioSource soundsAudioSource;
    [SerializeField]
    private AudioSource musicAudioSource;
    [SerializeField]
    private AudioSource ambientAudioSource;

    public void PlayEnvironmentSound(AudioClip clip)
    {
        PlayOneShotSound(clip, soundsAudioSource);
    }

    public void PlayMusic(AudioClip clip)
    {
        AddToAudioSourceAndPlay(clip, musicAudioSource);
    }

    public void PlayAmbient(AudioClip clip)
    {
        AddToAudioSourceAndPlay(clip, ambientAudioSource);
    }

    private void PlayOneShotSound(AudioClip clip, AudioSource audioSource)
    {
        if(clip)
        {
            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("There is no audio clip!");
        }
    }

    private void AddToAudioSourceAndPlay(AudioClip clip, AudioSource audioSource)
    {
        if(clip)
        {
            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("There is no audio clip!");
        }
    }
}
