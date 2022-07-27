using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static AudioManager instance;
    private AudioSource _audioSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
