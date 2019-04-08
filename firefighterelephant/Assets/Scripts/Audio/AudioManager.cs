using System.Collections.Generic;
using UnityEngine;

public enum SoundTypes { SFX, Music };
public class AudioManager : SingletonOfType<AudioManager>
{
    [SerializeField] private AudioSource srcBaseSFX;
    [SerializeField] private AudioSource srcBaseMusic;

    [SerializeField] private List<AudioSource> AudioSourceList = new List<AudioSource>();

    public void PlaySound(SoundTypes type, AudioClip clip, bool loop = false, float pitch = 1f)
    {
        AudioSource src = new AudioSource();
        switch(type)
        {
            case SoundTypes.Music:
                src = Instantiate(srcBaseMusic, transform);
                break;
            case SoundTypes.SFX:
                src = Instantiate(srcBaseSFX, transform);
                break;
        }
        src.clip = clip;
        src.loop = loop;
        src.pitch = pitch;
        src.Play();
        if (!loop)
            Destroy(src.gameObject, clip.length);
        else
            AudioSourceList.Add(src);
    }

    private void Start()
    {
        PlaySound(SoundTypes.Music, Sounds.library.genericBGM);
    }
}