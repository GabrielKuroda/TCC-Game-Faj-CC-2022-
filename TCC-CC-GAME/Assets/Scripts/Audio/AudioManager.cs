using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : IPersistentSingleton<AudioManager>
{
    // Start is called before the first frame update

    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        DontDestroyOnLoad(_audioSource);
    }
    public void PlayBGM(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
