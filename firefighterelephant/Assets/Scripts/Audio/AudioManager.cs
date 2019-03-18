using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundTypes { SFX, Music };

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource srcBaseSFX;
    [SerializeField] private AudioSource srcBaseMusic;

    [SerializeField] private SoundSet[] soundSet;

    private void Awake()
    {
        for (var i = 0; i < soundSet.Length; i++)
        {
            var newSRC = Instantiate(srcBaseMusic, transform);
            newSRC.clip = soundSet[i].clip;
            newSRC.loop = soundSet[i].loop;
            newSRC.pitch = soundSet[i].pitch;
            if(soundSet[i].stop)
                newSRC.Stop();
            else
                newSRC.Play();
        }
        for (var i = 0; i < soundSet.Length; i++)
        {
            var newSRC = Instantiate(srcBaseSFX, transform);
            newSRC.clip = soundSet[i].clip;
            newSRC.loop = soundSet[i].loop;
            newSRC.pitch = soundSet[i].pitch;
        }
    }

    public void PlaySound()
    {

    }
}

[Serializable]
public class SoundSet
{
    public SoundEvent eventType;
    public SoundTypes soundType;
    public AudioClip clip;
    [Range(-3, 3)] public float pitch = 1;
    public bool loop;
    public bool stop;
}