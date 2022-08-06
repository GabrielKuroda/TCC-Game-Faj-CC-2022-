using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : IPersistentSingleton<AudioManager>
{
    // Start is called before the first frame update

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayBGM(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
