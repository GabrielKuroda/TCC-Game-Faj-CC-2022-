using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    private AudioManager audioM;
    void Start()
    {
        audioM = FindObjectOfType<AudioManager>();
        audioM.PlayBGM(clip);
    }
}
