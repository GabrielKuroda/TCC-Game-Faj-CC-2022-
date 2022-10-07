using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    
    void Start()
    {
        AudioManager.Instance.PlayBGM(clip);
    }
}
