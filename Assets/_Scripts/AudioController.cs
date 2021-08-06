using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioController : MonoBehaviour
{

    private static readonly string MusicPrefs = "MusicPrefs";
    private static readonly string SoundEffectsPrefs = "SoundEffectsPrefs";
    
    public AudioSource musicSource;
    public AudioSource[] sfxSource;
    
    private readonly float[] musicStarts = {0, 11.99f, 35.99f, 47.98f};

    private void Awake()
    {
        SetAudioSettings();
    }


    private void SetAudioSettings()
    {
        if (musicSource != null)
        {
            musicSource.mute = PlayerPrefs.GetInt(MusicPrefs) == 1;
            musicSource.time = musicStarts[Random.Range(0,4)];
        }
        
        for (int i = 0; i < sfxSource.Length; i++)
        {
            sfxSource[i].mute = PlayerPrefs.GetInt(SoundEffectsPrefs) == 1;
        }
    }
    
    public void PlayEnemyDeath()
    {
        if (!sfxSource[0].isPlaying)
        {
            sfxSource[0].time = Mathf.Min(0, sfxSource[0].clip.length);
            sfxSource[0].Play();
        }
    }

    public void PlayPlayerHit()
    {
        sfxSource[1].time = Mathf.Min(0, sfxSource[1].clip.length);
        sfxSource[1].Play();
    }

    public void PlayPlayerLoss()
    {
        sfxSource[2].time = Mathf.Min(0, sfxSource[2].clip.length);
        sfxSource[2].Play();
    }
    
}
